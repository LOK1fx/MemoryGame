using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class ActiveButton : MonoBehaviour
    {
        [SerializeField] private bool _isTrap;

        [SerializeField] private Turret[] _turrets;

        [Space]
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private Vector3 _onPressPositionOffset;

        private Vector3 _defaultPosition;

        private void Start()
        {
            _defaultPosition = _visualTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Player.Player>(out var player))
            {
                if(_isTrap)
                {
                    foreach (var item in _turrets)
                    {
                        item.Shoot();
                    }
                }

                _visualTransform.position = _defaultPosition - _onPressPositionOffset;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _visualTransform.position = _defaultPosition;
            }
        }
    }
}
