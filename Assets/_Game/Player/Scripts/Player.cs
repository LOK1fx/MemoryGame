using System.Collections;
using UnityEngine;
using System;
using Cinemachine;
using System.Collections.Generic;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera), typeof(PlayerState))]
    [RequireComponent(typeof(Health), typeof(PlayerInteraction))]
    public class Player : Pawn, IDamagable
    {
        public event Action OnRespawned;
        public event Action OnDeath;
        public event Action OnTakeDamage;
        public event Action<DocInfo> OnTakeDocument;

        public PlayerMovement Movement { get; private set; }
        public PlayerCamera Camera { get; private set; }
        public PlayerState State { get; private set; }
        public FirstPersonArms FirstPersonArms => _firstPersonArms;
        public PlayerInteraction Interaction { get; private set; }
        public PlayerItemManager ItemManager { get; private set; }
        public Health Health { get; private set; }
        public bool IsDead { get; private set; }


        [SerializeField] private FirstPersonArms _firstPersonArms;
        [SerializeField] private Vector3 _crouchEyePosition;
        [SerializeField] private List<Renderer> _visuals;

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
            ItemManager = GetComponent<PlayerItemManager>();
            ItemManager.Construct(this);

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
            ItemManager.OnInput(this);

            if (Input.GetKey(KeyCode.Space))
                Movement.Jump();

            if(Input.GetKeyDown(KeyCode.LeftControl))
                Movement.StartCrouch();

            if(Input.GetKeyUp(KeyCode.LeftControl))
                if(Movement.CanStand())
                    Movement.StopCrouch();
        }

        private void OnLand()
        {
            Camera.AddCameraOffset(Vector3.down * 0.4f);
            Camera.TriggerRecoil(new Vector3(-2f, 0f, 1.5f));
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

        public void TakeDocument(DocInfo doc)
        {
            OnTakeDocument?.Invoke(doc);
        }

        public void PassIntroTransport()
        {
            Movement.StopMovementInput();
            Movement.SetAxisInput(Vector2.zero);

            Movement.Rigidbody.useGravity = false;

            Movement.Rigidbody.isKinematic = true;
            Movement.Rigidbody.velocity = Vector3.zero;
            Movement.PlayerCollider.enabled = false;
            State.SetInTransport(true);
        }

        public void DepassFromTransport()
        {
            Movement.StartMovementInput();
            Movement.SetAxisInput(Vector2.zero);

            Movement.Rigidbody.useGravity = true;

            Movement.Rigidbody.isKinematic = false;
            State.SetInTransport(false);
            Movement.PlayerCollider.enabled = true;
            Movement.Rigidbody.velocity = Vector3.zero;
        }

        public void HideVisuals()
        {
            foreach (var visual in _visuals)
            {
                visual.enabled = false;
            }
        }

        public void ShowVisuals()
        {
            foreach (var visual in _visuals)
            {
                visual.enabled = true;
            }
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

                Camera.UnlockCursor();

                //photonView.RPC(nameof(Respawn), RpcTarget.All, new object[3] { respawnPosition.x, respawnPosition.y, respawnPosition.z });
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(Movement.GetSpeed() > 8f)
            {
                Health.ReduceHealth(Health.MaxHp);
            }
        }

        private void Respawn(float respawnPositionX, float respawnPositionY, float respawnPositionZ) //Photon RPC don't serialize/deserialize Vector3 type
        {
            var respawnPosition = new Vector3(respawnPositionX, respawnPositionY, respawnPositionZ);

            Debug.DrawRay(respawnPosition, Vector3.up * 2f, Color.yellow, 1f, false);
        }
    }
}