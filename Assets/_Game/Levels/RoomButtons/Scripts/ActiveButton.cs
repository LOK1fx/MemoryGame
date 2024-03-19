using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class ActiveButton : MonoBehaviour
    {
        [SerializeField] private bool _isTrap;

        [SerializeField] private Turret[] _turrets;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Player.Player>(out var player) && _isTrap)
            {
                foreach (var item in _turrets)
                {
                    item.Shoot();
                }
            }
        }
    }
}
