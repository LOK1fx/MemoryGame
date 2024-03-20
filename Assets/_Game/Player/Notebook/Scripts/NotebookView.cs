using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotebookView : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPhotoItem;
    [SerializeField] private Transform _contentNotes;

    public void SpawnNote(PhotoConfig noteConfig)
    {
        AddedCall(_contentNotes);
        GameObject photoItem = Instantiate(_prefabPhotoItem, _contentNotes);
        PhotoItem scriptPhotoItem = photoItem.GetComponent<PhotoItem>();
        scriptPhotoItem.Initialized(noteConfig);
    }

    private void AddedCall(Transform content)
    {
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = content.GetComponent<RectTransform>();
        Vector2 cellSizeContent = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + grid.cellSize.y + grid.spacing.y);
        rectTransform.sizeDelta = cellSizeContent;
    }

    private void ClearAllCall(Transform content)
    {
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = content.GetComponent<RectTransform>();
        Vector2 cellSizeContent = new Vector2(0, rectTransform.sizeDelta.y);
        rectTransform.sizeDelta = cellSizeContent;
    }
}
