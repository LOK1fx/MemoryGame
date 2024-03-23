using UnityEngine;

namespace LOK1game
{
    public class PlayerMovementDisablerTrigger : MonoBehaviour
    {
        [SerializeField] private bool _canMove = false;

        private bool _activated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player) && _activated == false)
            {
                if (_canMove)
                    player.Movement.StartMovementInput();
                else
                    player.Movement.StopMovementInput();

                player.Movement.SetAxisInput(Vector2.zero);

                _activated = true;
            }
        }
    }
}