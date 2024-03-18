using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Player.Player))]
    public class PlayerAnimations : MonoBehaviour
    {
        private const string FLAG_WALKING = "IsWalking";
        private const string FLAG_AIMING = "IsAiming";
        private const string TRIG_TAKE_DOC = "TakeDoc";
        private const string TRIG_DEATH = "Death";

        [SerializeField] private Animator _armsAnimator;

        [Space]
        [SerializeField] private float _movementSpeedThreashold = 5f;

        private Player.Player _player;

        private void Start()
        {
            _player = GetComponent<Player.Player>();
            _player.OnDeath += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
        }

        private void Update()
        {
            _armsAnimator.SetBool(FLAG_AIMING, Input.GetKey(KeyCode.Mouse1));


            if(_player.Movement.GetSpeed() > _movementSpeedThreashold)
            {
                _armsAnimator.SetBool(FLAG_WALKING, true);
            }
            else
            {
                _armsAnimator.SetBool(FLAG_WALKING, false);
            }
        }

        public void PlayDocsTakeSequance()
        {
            _armsAnimator.SetTrigger(TRIG_TAKE_DOC);
        }

        private void OnPlayerDeath()
        {
            _armsAnimator.SetTrigger(TRIG_DEATH);
        }
    }
}