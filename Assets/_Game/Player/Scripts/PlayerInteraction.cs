using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerInteraction : MonoBehaviour, IInputabe
    {
        public event Action OnStartInteraction;
        public event Action<string> OnStartHighlithing;

        [SerializeField] private float _interactionLength;
        [SerializeField] private LayerMask _interactableLayerMask;

        //[SerializeField] private InteractionView _interactionView;

        private Player.Player _player;
        private Transform _cameraTransform;

        public void Construct(Player.Player player)
        {
            _player = player;
            _cameraTransform = _player.Camera.GetCameraTransform();
        }

        private void Update()
        {
            
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

                    interactable.OnHighlight();
                    OnStartHighlithing?.Invoke("Press F to take the document");
                }
            }   
        }

        /*
        private void InteractableRay()
        {
            bool isInteracteble = false;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                    _interactionLength, _interactableLayerMask, QueryTriggerInteraction.Collide))
            {
                
            }

            _interactionView.DisplayTextInteracrion(isInteracteble);
        }
        */
    }
}