using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LOK1game.UI
{
    [RequireComponent(typeof(Animator))]
    public class NoteNotification : MonoBehaviour
    {
        private const string SHOW_ANIM = "Notification_In";

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _light;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Show(string message, Color lightColor)
        {
            _animator.Play(SHOW_ANIM, 0, 0);
            _text.text = message;
            _light.color = lightColor;
        }
    }
}