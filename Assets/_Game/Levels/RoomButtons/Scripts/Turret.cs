using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabLine;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private PlayerSpawner _playerSpawner;

        private Player.Player _player;

        private void Awake()
        {
            _playerSpawner.OnSpanwedPlayer += GetPlayer;
        }

        private void GetPlayer(Player.Player player)
        {
            _player = player;
        }

        public void Shoot()
        {
            var line = Instantiate(_prefabLine, null);
            var lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, _firePoint.position);
            lineRenderer.SetPosition(1, _player.transform.position);

            _player.Health.ReduceHealth(100);
        }
    }
}
