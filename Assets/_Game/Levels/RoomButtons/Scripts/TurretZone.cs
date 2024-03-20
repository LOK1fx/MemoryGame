using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class TurretZone : MonoBehaviour
    {
        [SerializeField] private Turret[] _turrets;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                foreach (var item in _turrets)
                {
                    item.SetPlayer(player);
                }
            }
        }
    }
}
