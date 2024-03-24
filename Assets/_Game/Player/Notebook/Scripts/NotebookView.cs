using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NotebookView : MonoBehaviour
{
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
}
