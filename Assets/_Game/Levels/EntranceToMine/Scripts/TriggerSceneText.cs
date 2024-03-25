using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.UI;

namespace LOK1game
{
    public class TriggerSceneText : MonoBehaviour
    {
        [TextArea]
        [SerializeField] private string _message;

        [Space]
        [SerializeField] private PlayerSpawner _playerSpawner;


        private PlayerHUD _currentPlayerHUD;

        private void Start()
        {
            _playerSpawner.OnSpawnedPlayerHud += SetPlayerHUD;
        }

        private void OnDestroy()
        {
            _playerSpawner.OnSpawnedPlayerHud -= SetPlayerHUD;
            _currentPlayerHUD.OnTutorialHide -= OnTutorialHide;
        }

        private void SetPlayerHUD(IPlayerHud hud)
        {
            _currentPlayerHUD = (PlayerHUD)hud;

            _currentPlayerHUD.OnTutorialHide += OnTutorialHide;
        }

        private void OnTutorialHide(Player.Player player)
        {
            player.Camera.LockCursor();
            player.Movement.StartMovementInput();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _currentPlayerHUD.ShowTutorial(_message);

                player.Camera.UnlockCursor();
                player.Movement.SetAxisInput(Vector2.zero);
                player.Movement.StopMovementInput();
            }
        }
    }
}
