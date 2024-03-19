using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using TMPro;
using UnityEngine;

namespace LOK1game
{
    public class PlayerHUD : MonoBehaviour, IPlayerHud
    {
        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private TextMeshProUGUI _interactionText;

        private Player.Player _player;
        private PlayerController _controller;

        public void Bind(Player.Player player, PlayerController controller)
        {
            _player = player;
            _controller = controller;

            _player.OnDeath += OnPlayerDeath;
            _player.Interaction.OnStartHighlithing += OnPlayerStartedInteraction;
        }

        private void OnDestroy()
        {
            _player.OnDeath -= OnPlayerDeath;
            _player.Interaction.OnStartHighlithing -= OnPlayerStartedInteraction;
        }

        private void OnPlayerStartedInteraction(string tooltip)
        {
            _interactionText.text = tooltip;
            _interactionText.gameObject.SetActive(true);
        }

        private void OnPlayerDeath()
        {
            _deathScreen.SetActive(true);
        }
    }
}