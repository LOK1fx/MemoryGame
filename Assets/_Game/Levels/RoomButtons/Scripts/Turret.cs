using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabLine;
        [SerializeField] private Transform _firePoint;

        private Player.Player _player;

        public void SetPlayer(Player.Player player)
        {
            _player = player;
        }

        public void Shoot()
        {
            var line = Instantiate(_prefabLine, null);
            var lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, _firePoint.position);
            lineRenderer.SetPosition(1, _player.transform.position);

            _player.TakeDamage(new Damage(100));
        }
    }
}
