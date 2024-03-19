using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Notebook : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPhotoItem;
    [SerializeField] private Transform _contentNotes;

    private List<NoteConfig> _notes;

    public void AddedNote(NoteConfig noteConfig)
    {
        _notes.Add(noteConfig);

        var photoItem = Instantiate(_prefabPhotoItem, _contentNotes);
    }
}
