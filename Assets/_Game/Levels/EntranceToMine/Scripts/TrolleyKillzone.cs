using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    [RequireComponent(typeof(KillZone))]
    public class TrolleyKillzone : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;

        private Player.Player _player;

        private void Awake()
        {
            _playerSpawner.OnSpawnedPlayer += OnPlayerSpawned;
        }

        private void OnDestroy()
        {
            _playerSpawner.OnSpawnedPlayer -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(Player.Player player)
        {
            _player = player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Trolley>(out var trolley))
            {
                if (trolley.PlayerInTrolley != null)
                    return;

                _player.TakeDamage(new Damage(100));
            }
        }
    }
}