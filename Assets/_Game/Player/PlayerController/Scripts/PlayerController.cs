using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerController : Controller
    {
        public event Action OnEscapePressed;
        public event Action OnPhotosAlbumPressed;
        public event Action OnDocumentsPressed;

        private bool _inPauseMenu;

        protected override void Awake()
        {
            
        }

        protected override void Update()
        {
            if (_inPauseMenu == false)
                ControlledPawn?.OnInput(this);

            //Right alt for editor tests
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _inPauseMenu = !_inPauseMenu;

                OnEscapePressed?.Invoke();
            }

            if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.H)) && _inPauseMenu == false)
            {
                OnPhotosAlbumPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                OnDocumentsPressed?.Invoke();
            }
        }
    }
}