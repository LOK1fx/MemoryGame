using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    public class PlayerHUD : MonoBehaviour, IPlayerHud
    {
        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private RawImage _currentPhotoPreview;
        [SerializeField] private TextMeshProUGUI _currentNoteText;
        [SerializeField] private GameObject _pauseMenu;

        private Player.Player _player;
        private PlayerController _controller;

        public void Bind(Player.Player player, PlayerController controller)
        {
            _player = player;
            _controller = controller;

            _player.OnDeath += OnPlayerDeath;
            _player.Interaction.OnStartHighlithing += OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken += OnPlayerTakedPhoto;

            _controller.OnEscapePressed += OnEscapePressed;
        }
        

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
            _player.Interaction.OnStartHighlithing -= OnPlayerStartedInteraction;
            _player.ItemManager.OnPhotoTaken -= OnPlayerTakedPhoto;

            _controller.OnEscapePressed -= OnEscapePressed;
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

        private void OnPlayerTakedPhoto(Texture2D photo, string note)
        {
            _currentPhotoPreview.texture = photo;
            _currentNoteText.text = note;
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
    }
}