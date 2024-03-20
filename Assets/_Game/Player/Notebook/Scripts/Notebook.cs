using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Notebook : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPhotoItem;
    [SerializeField] private Transform _contentNotes;

    private Dictionary<NoteConfig, GameObject> _notes;

    public void AddedNote(NoteConfig noteConfig)
    {
        var photoItem = Instantiate(_prefabPhotoItem, _contentNotes);
        var photoItemView = photoItem.GetComponent<PhotoItemView>();
        photoItemView.DisplayPhoto(noteConfig.Photo);

        _notes.Add(noteConfig, photoItem);
    }
}
