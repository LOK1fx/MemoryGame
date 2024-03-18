using System.Collections;
using UnityEngine;
using System;
using Cinemachine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera), typeof(PlayerState))]
    [RequireComponent(typeof(Health), typeof(PlayerInteraction))]
    public class Player : Pawn, IDamagable
    {
        public event Action OnRespawned;
        public event Action OnDeath;
        public event Action OnTakeDamage;

        public PlayerMovement Movement { get; private set; }
        public PlayerCamera Camera { get; private set; }
        public PlayerState State { get; private set; }
        public FirstPersonArms FirstPersonArms => _firstPersonArms;
        public PlayerInteraction Interaction { get; private set; }
        public Health Health { get; private set; }
        public bool IsDead { get; private set; }


        [SerializeField] private FirstPersonArms _firstPersonArms;
        [SerializeField] private Vector3 _crouchEyePosition;

        private Vector3 _defaultEyePosition;

        [Space]
        [SerializeField] private Vector3 _onDamageCameraPunch;


        private void Awake()
        {
            Health = GetComponent<Health>();
            Movement = GetComponent<PlayerMovement>();
            Camera = GetComponent<PlayerCamera>();
            Camera.Construct(this);
            State = GetComponent<PlayerState>();
            Interaction = GetComponent<PlayerInteraction>();
            Interaction.Construct(this);

            Movement.OnLand += OnLand;
            Movement.OnJump += OnJump;
            Movement.OnStartCrouch += OnStartCrouching;
            Movement.OnStopCrouch += OnStopCrouching;

            _defaultEyePosition = Camera.GetCameraTransform().localPosition;
        }

        private void OnDestroy()
        {
            Movement.OnLand -= OnLand;
            Movement.OnJump -= OnJump;
            Movement.OnStartCrouch -= OnStartCrouching;
            Movement.OnStopCrouch -= OnStopCrouching;
        }

        private void Start()
        {
            if(IsLocal == false)
            {
                playerType = EPlayerType.World;
            }
            else
            {
                playerType = EPlayerType.View;
            }
        }

        private void Update()
        {
            var cameraRotation = Camera.GetCameraTransform().eulerAngles;

            Movement.DirectionTransform.rotation = Quaternion.Euler(0f, cameraRotation.y, 0f);
        }

        public override void OnInput(object sender)
        {
            if (IsLocal == false || IsDead)
                return;

            var inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Camera.OnInput(this);
            Movement.SetAxisInput(inputAxis);
            Interaction.OnInput(this);

            if (Input.GetKey(KeyCode.Space))
                Movement.Jump();

            if(Input.GetKeyDown(KeyCode.LeftControl))
                Movement.StartCrouch();

            if(Input.GetKeyUp(KeyCode.LeftControl))
                if(Movement.CanStand())
                    Movement.StopCrouch();

            if (Input.GetKeyDown(KeyCode.K))
                TakeDamage(new Damage(Health.MaxHp));

            if (Input.GetKeyDown(KeyCode.U))
                TakeDamage(new Damage(15));
        }

        private void OnLand()
        {
            Camera.AddCameraOffset(Vector3.down * 0.5f);
            Camera.TriggerRecoil(new Vector3(-1f, 0f, 1f));
        }

        private void OnJump()
        {
            Camera.AddCameraOffset(Vector3.up * 0.125f);
            Camera.TriggerRecoil(new Vector3(-4f, 0f, 1f));
        }

        private void OnStartCrouching()
        {
            Camera.DesiredPosition = _crouchEyePosition;
        }

        private void OnStopCrouching()
        {
            Camera.DesiredPosition = _defaultEyePosition;
        }

        public void TakeDamage(Damage damage)
        {
            if (IsDead || damage.Value <= 0)
                return;


            RemoveHealth(damage.Value);
            TakeDamageReplacatedEffects();
        }

        private void TakeDamageReplacatedEffects()
        {
            Camera.TriggerRecoil(_onDamageCameraPunch);

            OnTakeDamage?.Invoke();
        }

        private void AddHealth(int value)
        {
            Health.AddHealth(value);
        }

        private void RemoveHealth(int value)
        {
            Health.ReduceHealth(value);

            if (Health.Hp <= 0)
                Death();
        }

        private void SetHealth(int value)
        {
            Health.SetHealth(value);
        }

        private void Death()
        {
            if (IsDead)
                return;

            IsDead = true;
            Movement.Rigidbody.isKinematic = true;
            Movement.PlayerCollider.enabled = false;

            OnDeath?.Invoke();

            if(IsLocal)
            {
                var respawnPosition = GetRandomSpawnPosition(true);

                //photonView.RPC(nameof(Respawn), RpcTarget.All, new object[3] { respawnPosition.x, respawnPosition.y, respawnPosition.z });
            }
        }


        private void Respawn(float respawnPositionX, float respawnPositionY, float respawnPositionZ) //Photon RPC don't serialize/deserialize Vector3 type
        {
            var respawnPosition = new Vector3(respawnPositionX, respawnPositionY, respawnPositionZ);

            Debug.DrawRay(respawnPosition, Vector3.up * 2f, Color.yellow, 1f, false);
        }
    }
}