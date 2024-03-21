using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotebookView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private GameObject _prefabPhotoItem;
    [SerializeField] private Transform _contentPhotos;

    private Dictionary<PhotoConfig, PhotoItem> _photoItems = new Dictionary<PhotoConfig, PhotoItem>();

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

    public void SpawnNote(PhotoConfig photoConfig)
    {
        AddedCall(_contentPhotos);
        GameObject photoItem = Instantiate(_prefabPhotoItem, _contentPhotos);
        PhotoItem scriptPhotoItem = photoItem.GetComponent<PhotoItem>();
        if(!_photoItems.ContainsKey(photoConfig)) _photoItems.Add(photoConfig, scriptPhotoItem);
        scriptPhotoItem.Initialized(photoConfig);
    }

    public void EnableHiddenText(PhotoConfig photoConfig)
    {
        _photoItems[photoConfig].EnableHiddenText(photoConfig.HiddenText);
    }

    public void ClearAllCall()
    {
        GridLayoutGroup grid = _contentPhotos.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = _contentPhotos.GetComponent<RectTransform>();
        Vector2 cellSizeContent = new Vector2(rectTransform.sizeDelta.x, 0);
        rectTransform.sizeDelta = cellSizeContent;
    }
    public void ClearChildren()
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[_contentPhotos.childCount];

        foreach (Transform child in _contentPhotos)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void AddedCall(Transform content)
    {
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = content.GetComponent<RectTransform>();
        Vector2 cellSizeContent = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + grid.cellSize.y + grid.spacing.y);
        rectTransform.sizeDelta = cellSizeContent;
    }
}
