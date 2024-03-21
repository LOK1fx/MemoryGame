using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class TriggerHiddenText : MonoBehaviour
    {
        [SerializeField] private int _idPhoto;
        [SerializeField] private PlayerSpawner _playerSpawner;

        private IPlayerHud _playerHud;
        private void Start()
        {
            _playerSpawner.OnSpawnedPlayerHud += GetPlayerHud;

        }

        private void GetPlayerHud(IPlayerHud playerHud)
        {
            _playerHud = playerHud;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _playerHud.EnableHiddenText(_idPhoto);
            }
        }

        private void OnDestroy()
        {
            _playerSpawner.OnSpawnedPlayerHud -= GetPlayerHud;
        }
    }
}
