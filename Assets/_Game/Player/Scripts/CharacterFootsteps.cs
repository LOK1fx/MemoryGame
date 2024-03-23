using LOK1game.Player;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Character.Generic
{
    [RequireComponent(typeof(PlayerState), typeof(Player.Player))]
    public class CharacterFootsteps : MonoBehaviour
    {
        public float ClipsRate = 0.25f;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private List<FootstepCollectionData> _footstepsCollection = new List<FootstepCollectionData>();

        private FootstepCollectionData _currentData;
        private PlayerState _state;
        private Player.Player _player;
        private float _clipTimer;

        private void Awake()
        {
            _currentData = _footstepsCollection[0];
            _state = GetComponent<PlayerState>();
            _player = GetComponent<Player.Player>();
        }

        private void Start()
        {
            _player.Movement.OnJump += OnPlayerJump;
            _player.Movement.OnLand += OnPlayerLand;
        }
        private void OnDestroy()
        {
            _player.Movement.OnJump -= OnPlayerJump;
            _player.Movement.OnLand -= OnPlayerLand;
        }

        private void OnPlayerJump()
        {
            var jumpSound = CreateFootstep(transform.position);

            jumpSound.PlayJump();
        }

        private void OnPlayerLand()
        {
            var landSound = CreateFootstep(transform.position);

            landSound.PlayLand();
        }


        private void Update()
        {
            if (_state.OnGround == false || _state.IsMoving() == false)
                return;

            _clipTimer += Time.deltaTime;

            if(_clipTimer >= ClipsRate)
            {
                if(Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out var hit, 1f, _groundLayer, QueryTriggerInteraction.Collide))
                {
                    var footstep = CreateFootstep(hit.point);

                    footstep.PlaySound();
                }

                _clipTimer = 0;
            }
        }

        private Footstep CreateFootstep(Vector3 position)
        {
            var footstep = Instantiate(_currentData.FootstepPrefab, position, Quaternion.identity);
            footstep.Construct(_currentData);


            return footstep;
        }
    }
}