using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.UI;
namespace LOK1game
{
    public class TriggerSceneText : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;

        private PlayerHUD _currentPlayerHUD;

        private void Start()
        {
            _playerSpawner.OnSpawnedPlayerHud += SetPlayerHUD;
        }

        private void SetPlayerHUD(IPlayerHud hud)
        {
            _currentPlayerHUD = (PlayerHUD)hud;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                
            }
        }

        private void OnDestroy()
        {
            _playerSpawner.OnSpawnedPlayerHud -= SetPlayerHUD;
        }
    }
}
