using UnityEngine;

namespace LOK1game
{
    [System.Serializable]
    public class DocInfo
    {
        public string DocName;

        [TextArea]
        public string Note;
    }

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Outline))]

    public class Doc : MonoBehaviour, IInteractable
    {
        public DocInfo Info => _info;

        [SerializeField] private DocInfo _info = new DocInfo();

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
            sender.TakeDocument(Info);

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

        public string GetTooltip()
        {
            return LocalisationSystem.GetLocalisedValue("tooltip_take_doc");
        }
    }
}