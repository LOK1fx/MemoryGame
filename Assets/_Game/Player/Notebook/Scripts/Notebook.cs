using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class Notebook : MonoBehaviour
    {
        [SerializeField] private PhotosView _photosView;
        [SerializeField] private DocumentsView _documentsView;
        [SerializeField] private NotebookView _notebookView;

        private List<PhotoConfig> _photos = new List<PhotoConfig>();
        private DocInfo _currentDocInfo;

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
            _photosView.SpawnNote(noteConfig);
        }

        public void SpawnAllPhoto()
        {
            _photosView.ClearChildren();
            _photosView.ClearAllCall();
            foreach (var item in _photos)
            {
                if(item.TypePhoto == _typePhoto)
                {
                    _photosView.SpawnNote(item);
                }
            }
        }

        public void EnableHiddenText(int id)
        {
            foreach (var item in _photos)
            {
                if (item.TypePhoto == ETypePhoto.Important && item.IdPhoto == id)
                {
                    _photosView.EnableHiddenText(item);
                }
            }
        }

        public void SetTypePhoto(int index)
        {
            _typePhoto = (ETypePhoto)index;
            SpawnAllPhoto();
        }

        public void SetDocInfo(DocInfo docInfo)
        {
            _currentDocInfo = docInfo;
        }

        public void IsActivePhotosView(bool isActive)
        {
            if (isActive)
            {
                _photosView.Show();
                _notebookView.Show();
            }
            else
            {
                _photosView.Hide();
                _notebookView.Hide();
            }
        }

        public void IsActiveDocumentsView(bool isActive)
        {
            if (isActive)
            {
                _documentsView.Show();
                _notebookView.Show();
            }
            else
            {
                _documentsView.Hide();
                _notebookView.Hide();
            }
            _documentsView.DisplayDocument(_currentDocInfo);
        }


        private void OnDestroy()
        {
            _player.ItemManager.OnPhotoTaken -= AddedPhoto;
        }

    }
}
