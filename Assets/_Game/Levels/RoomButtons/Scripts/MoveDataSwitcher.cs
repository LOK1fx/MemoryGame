using LOK1game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class MoveDataSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerMovementParams _playerMovementParams;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                player.Movement.SetMoveData(_playerMovementParams);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                player.Movement.SetDeafaultMoveData();
            }
        }
    }
}
