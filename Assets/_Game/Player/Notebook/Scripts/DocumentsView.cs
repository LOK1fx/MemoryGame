using UnityEngine;
using TMPro;

namespace LOK1game
{
    public class DocumentsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textDocument;
        private CanvasGroup _canvas;

        private void Start()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            _canvas.alpha = 1f;
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvas.alpha = 0f;
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
        }

        public void DisplayDocument(DocInfo info)
        {
            if (info == null) return;
            _textTitle.text = LocalisationSystem.GetLocalisedValue(info.DocName);
            _textDocument.text = LocalisationSystem.GetLocalisedValue(info.Note);
        }
    }
}
