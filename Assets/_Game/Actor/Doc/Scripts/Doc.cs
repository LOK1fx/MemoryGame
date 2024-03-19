using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class Doc : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _inHandDocModel;

        private Collider _interactionCollider;

        private void Start()
        {
            _interactionCollider = GetComponent<Collider>();
        }

        public void OnInteract(Player.Player sender)
        {
            gameObject.SetActive(false);

            var socket = sender.FirstPersonArms.RightHandSocket;
            var inHandObject = Instantiate(_inHandDocModel, socket);

            Destroy(inHandObject, 1f);

            sender.GetComponent<PlayerAnimations>().PlayDocsTakeSequance();
        }

        public void OnHighlight()
        {
            Debug.Log("awd");
        }
    }
}