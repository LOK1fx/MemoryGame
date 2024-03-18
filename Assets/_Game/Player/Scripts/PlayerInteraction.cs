using UnityEngine;

namespace LOK1game
{
    public class PlayerInteraction : MonoBehaviour, IInputabe
    {
        [SerializeField] private float _interactionLength;
        [SerializeField] private LayerMask _interactableLayerMask;

        private Player.Player _player;
        private Transform _cameraTransform;

        public void Construct(Player.Player player)
        {
            _player = player;
            _cameraTransform = _player.Camera.GetCameraTransform();
        }

        public void OnInput(object sender)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                    _interactionLength, _interactableLayerMask, QueryTriggerInteraction.Collide))
                {
                    if(hit.collider.gameObject.TryGetComponent<IInteractable>(out var interactable))
                    {
                        interactable.OnInteract(_player);
                    }
                }

                Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _interactionLength, Color.green, 1f);
            }
        }
    }
}