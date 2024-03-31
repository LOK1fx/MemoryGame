using LOK1game;
using LOK1game.Player;
using System.Linq;
using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerSpawner : MonoBehaviour
    {
        public event Action<Player.Player> OnSpawnedPlayer;
        public event Action<IPlayerHud> OnSpawnedPlayerHud;
        public event Action<GameObject> OnPlayerCameraSpawned;
        public event Action<PlayerController> OnPlayerControllerSpawned;

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _playerCamera;
        [SerializeField] private GameObject _playerHud;

        private Player.Player _currentPlayer;
        private PlayerController _currentPlayerController;

        private void Start()
        {
            var spawnPoint = GetSpawnPointTansform();
            var player = Instantiate(_player, spawnPoint.position, Quaternion.identity);

            var camera = Instantiate(_playerCamera, Vector3.zero, Quaternion.identity);
            OnPlayerCameraSpawned?.Invoke(camera);

            var hud = Instantiate(_playerHud, Vector3.zero, Quaternion.identity);

            _currentPlayer = player.GetComponent<Player.Player>();
            _currentPlayer.SetRotation(spawnPoint.rotation);

            _currentPlayerController = Controller.Create<PlayerController>(_currentPlayer);

            OnSpawnedPlayer?.Invoke(_currentPlayer);
            OnPlayerControllerSpawned?.Invoke(_currentPlayerController);

            hud.GetComponent<IPlayerHud>().Bind(player.GetComponent<Player.Player>(), _currentPlayerController);
            OnSpawnedPlayerHud?.Invoke(hud.GetComponent<IPlayerHud>());
        }

        private void OnDestroy()
        {
            Controller.ClearControllers();
        }

        private Transform GetSpawnPointTansform()
        {
            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (spawnPoints.Count == 0)
                return null;

            if (spawnPoints.Count < 1)
                return null;

            var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];

            return spawnPoint.transform;
        }
    }
}