using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Player.Player))]
    public class PlayerAnimations : MonoBehaviour
    {
        private const string FLAG_WALKING = "IsWalking";
        private const string FLAG_AIMING = "IsAiming";

        [SerializeField] private Animator _armsAnimator;

        [Space]
        [SerializeField] private float _movementSpeedThreashold = 5f;

        private Player.Player _player;

        private void Start()
        {
            _player = GetComponent<Player.Player>();
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
    }
}