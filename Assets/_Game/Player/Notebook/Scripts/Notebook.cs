using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class Notebook : MonoBehaviour
    {
        [SerializeField] private NotebookView _notebookView;

        private List<PhotoConfig> _photos = new List<PhotoConfig>();
        private Player.Player _player;

        private ETypePhoto _typePhoto;

        public void Initialized(Player.Player player)
        {
            _player = player;
            _player.ItemManager.OnPhotoTaken += AddedPhoto;
        }

        public void AddedPhoto(PhotoConfig noteConfig)
        {
            _photos.Add(noteConfig);
            if (noteConfig.TypePhoto != _typePhoto) return;
            _notebookView.SpawnNote(noteConfig);
        }

        public void SpawnAllPhoto()
        {
            _notebookView.ClearChildren();
            _notebookView.ClearAllCall();
            foreach (var item in _photos)
            {
                if(item.TypePhoto == _typePhoto)
                {
                    _notebookView.SpawnNote(item);
                }
            }
        }

        public void EnableHiddenText(int id)
        {
            foreach (var item in _photos)
            {
                if (item.TypePhoto == ETypePhoto.Important && item.IdPhoto == id)
                {
                    _notebookView.EnableHiddenText(item);
                }
            }
        }

        public void SetTypePhoto(int index)
        {
            _typePhoto = (ETypePhoto)index;
            SpawnAllPhoto();
        }


        private void OnDestroy()
        {
            _player.ItemManager.OnPhotoTaken -= AddedPhoto;
        }

    }
}
