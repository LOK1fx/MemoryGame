using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game.UI
{
    public class PlayerHUD : MonoBehaviour, IPlayerHud
    {
        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Notebook _notebook;
        [SerializeField] private NoteNotification _noteNotification;
        
        private Player.Player _player;
        private PlayerController _controller;

        private bool _isPhotosAlbumOpen;

        public void Bind(Player.Player player, PlayerController controller)
        {
            _player = player;
            _controller = controller;

            _notebook.Initialized(player);

            _player.OnDeath += OnPlayerDeath;
            _player.Interaction.OnStartHighlithing += OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken += OnPlayerPhotoTaken;

            _controller.OnEscapePressed += OnEscapePressed;
            _controller.OnPhotosAlbumPressed += OnPhotosAlbumOpen;
        }

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
            _player.Interaction.OnStartHighlithing -= OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken -= OnPlayerPhotoTaken;

            _controller.OnEscapePressed -= OnEscapePressed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse2) && _player.IsDead == false)
            {
                //temporary, i think
                if (_isPhotosAlbumOpen == true && Cursor.lockState == CursorLockMode.Locked)
                    _player.Camera.UnlockCursor();
                else
                    _player.Camera.LockCursor();
            }
        }

        private void OnPlayerPhotoTaken(PhotoConfig config)
        {
            if (config.TypePhoto == ETypePhoto.Important || config.TypePhoto == ETypePhoto.Noted)
            {
                _noteNotification.Show("Importnant photo added to notes. Press Tab or H", Color.red);
            }
        }

        private void OnPlayerStartedInteraction(string tooltip, bool isActive)
        {
            _interactionText.text = tooltip;
            _interactionText.gameObject.SetActive(isActive);
        }

        private void OnPlayerDeath()
        {
            _deathScreen.SetActive(true);
        }

        private void OnEscapePressed()
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            //temporary, i think
            if (_pauseMenu.activeSelf)
                _player.Camera.UnlockCursor();
            else
                _player.Camera.LockCursor();
        }

        public void EnableHiddenText(int idPhoto)
        {
            _notebook.EnableHiddenText(idPhoto);
        }

        private void OnPhotosAlbumOpen()
        {
            if (_player.IsDead == true)
                return;

            _isPhotosAlbumOpen = !_isPhotosAlbumOpen;

            if (_isPhotosAlbumOpen)
                _notebook.View.Show();
            else
                _notebook.View.Hide();
        }
    }
}