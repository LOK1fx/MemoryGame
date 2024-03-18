using LOK1game;
using LOK1game.Player;
using System.Linq;
using UnityEngine;

namespace LOK1game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _playerCamera;

        private Player.Player _currentPlayer;

        private void Start()
        {
            var player = Instantiate(_player, GetSpawnPointPosition(), Quaternion.identity);
            var camera = Instantiate(_playerCamera, Vector3.zero, Quaternion.identity);

            _currentPlayer = player.GetComponent<Player.Player>();

            Controller.Create<PlayerController>(_currentPlayer);
        }

        private Vector3 GetSpawnPointPosition()
        {
            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (spawnPoints.Count == 0)
                return Vector3.zero;

            if (spawnPoints.Count < 1)
                return Vector3.zero;

            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            return spawnPoint.transform.position;
        }
    }
}