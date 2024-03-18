using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerInputProvider : MonoBehaviour
    {
        [SerializeField] private List<IInputabe> _inputables = new List<IInputabe>();

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void Add(IInputabe inputabe)
        {
            _inputables.Add(inputabe);
        }

        private void Update()
        {
            if (_player != null && _player.IsLocal == true)
                return;

            foreach (var inputable in _inputables)
            {
                inputable.OnInput(_player);
            }
        }
    }
}