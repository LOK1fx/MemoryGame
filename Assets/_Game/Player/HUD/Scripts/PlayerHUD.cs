using System;
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
        public event Action<Player.Player> OnTutorialHide;

        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Notebook _notebook;
        [SerializeField] private NoteNotification _noteNotification;
        [SerializeField] private CanvasGroupFade _tutorialCanvas;
        [SerializeField] private TextMeshProUGUI _tutorialText;
        
        private Player.Player _player;
        private PlayerController _controller;

        private bool _isPhotosAlbumOpen;
        private bool _isDocumentsOpen;
        private bool _isTutorialShowing;

        public void Bind(Player.Player player, PlayerController controller)
        {
            _player = player;
            _controller = controller;

            _notebook.Initialized(player);

            _player.OnDeath += OnPlayerDeath;
            _player.Interaction.OnStartHighlithing += OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken += OnPlayerPhotoTaken;
            _player.OnTakeDocument += OnPlayerTakeDocument;

            _controller.OnEscapePressed += OnEscapePressed;
            _controller.OnPhotosAlbumPressed += OnPhotosAlbumOpen;
            _controller.OnDocumentsPressed += OnDocumentOpen;
        }

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
            _player.Interaction.OnStartHighlithing -= OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken -= OnPlayerPhotoTaken;
            _player.OnTakeDocument -= OnPlayerTakeDocument;

            _controller.OnEscapePressed -= OnEscapePressed;
            _controller.OnPhotosAlbumPressed -= OnPhotosAlbumOpen;
            _controller.OnDocumentsPressed -= OnDocumentOpen;
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

        public void ShowTutorial(string message)
        {
            _tutorialCanvas.Show();
            _tutorialText.text = LocalisationSystem.GetLocalisedValue(message);

            _isTutorialShowing = true;
        }

        public void HideTutorial()
        {
            _tutorialCanvas.InstaHide();
            _isTutorialShowing = false;

            OnTutorialHide?.Invoke(_player);
        }

        private void OnPlayerPhotoTaken(PhotoConfig config)
        {
            var color = Color.white;
            var photoTypeDescription = "";

            if (config.TypePhoto == ETypePhoto.Important)
            {
                color = Color.red;
                photoTypeDescription = LocalisationSystem.GetLocalisedValue("photo_description_important");
            }  
            else if (config.TypePhoto == ETypePhoto.Noted)
            {
                color = Color.cyan;
                photoTypeDescription = LocalisationSystem.GetLocalisedValue("photo_description_noted");
            } 
            else
            {
                return;
            }

            _noteNotification.Show(string.Format(LocalisationSystem.GetLocalisedValue("notification_photo"), photoTypeDescription), color);
        }

        private void OnPlayerTakeDocument(DocInfo doc)
        {
            _noteNotification.Show(string.Format(LocalisationSystem.GetLocalisedValue("notification_doc"),
                LocalisationSystem.GetLocalisedValue(doc.DocName)), Color.yellow);

            _notebook.SetDocInfo(doc);
        }

        private void OnPlayerStartedInteraction(string tooltip, bool isActive)
        {
            _interactionText.text = tooltip;
            _interactionText.gameObject.SetActive(isActive);
        }

        private void OnPlayerDeath()
        {
            _deathScreen.SetActive(true);
            _pauseMenu.SetActive(false);
        }

        private void OnEscapePressed()
        {
            if (_isTutorialShowing)
            {
                HideTutorial();

                return;
            }

            if (_notebook.IsShowing)
            {
                _notebook.IsActivePhotosView(false);

                return;
            }

            if (_player.IsDead)
                return;

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            //temporary, i think
            if (_pauseMenu.activeSelf)
            {
                _player.Camera.UnlockCursor();
                _player.Movement.SetAxisInput(Vector2.zero); //stops the player
                _controller.IsInputProcessing = false;
            } 
            else
            {
                _player.Camera.LockCursor();
                _controller.IsInputProcessing = true;
            }
        }

        public void EnableHiddenText(int idPhoto)
        {
            _notebook.EnableHiddenText(idPhoto);
        }

        private void OnPhotosAlbumOpen()
        {
            if (_player.IsDead == true) return;

            _isDocumentsOpen = false;
            _notebook.IsActiveDocumentsView(_isDocumentsOpen);

            _isPhotosAlbumOpen = !_isPhotosAlbumOpen;

            _notebook.IsActivePhotosView(_isPhotosAlbumOpen);
            
        }

        private void OnDocumentOpen()
        {
            if (_player.IsDead == true) return;

            _isPhotosAlbumOpen = false;
            _notebook.IsActivePhotosView(_isPhotosAlbumOpen);

            _isDocumentsOpen = !_isDocumentsOpen;

            _notebook.IsActiveDocumentsView(_isDocumentsOpen);
            
        }
    }
}