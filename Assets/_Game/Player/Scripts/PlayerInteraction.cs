using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerInteraction : MonoBehaviour, IInputabe
    {
        public event Action OnStartInteraction;
        public event Action<string, bool> OnStartHighlithing;

        [SerializeField] private float _interactionLength;
        [SerializeField] private LayerMask _interactableLayerMask;

        private Player.Player _player;
        private Transform _cameraTransform;

        private IInteractable _currentInteractable;

        public void Construct(Player.Player player)
        {
            _player = player;
            _cameraTransform = _player.Camera.GetCameraTransform();
        }

        public void OnInput(object sender)
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                    _interactionLength, _interactableLayerMask, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.gameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        interactable.OnInteract(_player);
                        OnStartInteraction?.Invoke();

                        Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _interactionLength, Color.green, 1f);
                    }

                    InteractableEnter(interactable);
                }
                else
                {
                    InteractableExit();
                }
            }
            else InteractableExit();
        }

        private void InteractableEnter(IInteractable interactable)
        {
            if (interactable != _currentInteractable)
            {
                if (_currentInteractable != null)
                {
                    _currentInteractable.OnHighlight(false);
                    OnStartHighlithing?.Invoke(_currentInteractable.GetTooltip(), false);
                }
                _currentInteractable = interactable;
                _currentInteractable.OnHighlight(true);
                OnStartHighlithing?.Invoke(_currentInteractable.GetTooltip(), true);
            }
        }

        private void InteractableExit()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.OnHighlight(false);
                OnStartHighlithing?.Invoke(_currentInteractable.GetTooltip(), false);
                _currentInteractable = null;
            }
        }

    }
}