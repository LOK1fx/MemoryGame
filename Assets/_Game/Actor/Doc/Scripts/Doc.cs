using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Outline))]

    public class Doc : MonoBehaviour, IInteractable
    {
        public string Note => _note;

        [SerializeField] [TextArea] private string _note;

        [Space]
        [SerializeField] private GameObject _inHandDocModel;

        private Collider _interactionCollider;
        private Outline _outline;

        private void Start()
        {
            _interactionCollider = GetComponent<Collider>();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void OnInteract(Player.Player sender)
        {
            gameObject.SetActive(false);

            var socket = sender.FirstPersonArms.RightHandSocket;
            var inHandObject = Instantiate(_inHandDocModel, socket);

            Destroy(inHandObject, 1f);

            sender.GetComponent<PlayerAnimations>().PlayDocsTakeSequance();
        }

        public void OnHighlight(bool isActive)
        {
            _outline.enabled = isActive;
        }
    }
}