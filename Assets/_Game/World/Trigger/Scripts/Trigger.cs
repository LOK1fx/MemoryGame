using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private bool _triggerOnce;
        [SerializeField] private bool _activeOnEnable = true;

        [Space]
        public UnityEvent OnTriggerEnterEvent;
        public UnityEvent OnTriggerExitEvent;

        private bool _activated;

        private void Start()
        {
            _activated = !_activeOnEnable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(EnterCondition(other))
            {
                OnTriggerEnterEvent?.Invoke();

                if (_triggerOnce)
                    _activated = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player.Player>(out var player))
                OnTriggerExitEvent?.Invoke();
        }

        protected virtual bool EnterCondition(Collider other)
        {
            if (_activated)
                return false; 
            if (other.gameObject.TryGetComponent<Player.Player>(out var player))
                return true;
            else
                return false;
        }
    }
}