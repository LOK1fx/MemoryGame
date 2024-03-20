using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class Notebook : MonoBehaviour
    {
        [SerializeField] private NotebookView _notebookView;

        private List<PhotoConfig> _notes = new List<PhotoConfig>();
        private Player.Player _player;

        public void Initialized(Player.Player player)
        {
            _player = player;
            _player.ItemManager.OnPhotoTaken += AddedNote;
        }

        public void AddedNote(PhotoConfig noteConfig)
        {
            _notes.Add(noteConfig);
            _notebookView.SpawnNote(noteConfig);
        }

        private void OnDestroy()
        {
            _player.ItemManager.OnPhotoTaken -= AddedNote;
        }
    }
}
