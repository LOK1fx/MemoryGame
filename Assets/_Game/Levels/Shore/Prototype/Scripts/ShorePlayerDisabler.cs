using LOK1game.UI;
using UnityEngine;

namespace LOK1game
{
    public class ShorePlayerDisabler : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _spawner;

        private Player.Player _currentPlayer;
        private PlayerHUD _currentPlayerHUD;
        private GameObject _playerCamera;
        private PlayerController _controller;

        private void Awake()
        {
            _spawner.OnSpawnedPlayer += OnPlayerSpawned;
            _spawner.OnSpawnedPlayerHud += OnPlayerHUDSpawned;
            _spawner.OnPlayerCameraSpawned += OnPlayerCameraSpawned;
            _spawner.OnPlayerControllerSpawned += OnPlayerControllerSpawned;
        }

        private void OnDestroy()
        {
            _spawner.OnSpawnedPlayer -= OnPlayerSpawned;
            _spawner.OnSpawnedPlayerHud -= OnPlayerHUDSpawned;
            _spawner.OnPlayerCameraSpawned -= OnPlayerCameraSpawned;
            _spawner.OnPlayerControllerSpawned -= OnPlayerControllerSpawned;
        }

        private void OnPlayerControllerSpawned(PlayerController controller)
        {
            _controller = controller;
        }

        private void OnPlayerCameraSpawned(GameObject camera)
        {
            _playerCamera = camera;
        }

        private void OnPlayerHUDSpawned(IPlayerHud hud)
        {
            _currentPlayerHUD = (PlayerHUD)hud;
        }

        private void OnPlayerSpawned(Player.Player player)
        {
            _currentPlayer = player;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                player.Camera.UnlockCursor();

                Destroy(_currentPlayer.gameObject);
                Destroy(_currentPlayerHUD.gameObject);
                Destroy(_controller.gameObject);
                Destroy(_playerCamera.gameObject);
            }
        }
    }
}