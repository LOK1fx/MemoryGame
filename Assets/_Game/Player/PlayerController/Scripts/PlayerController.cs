using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerController : Controller
    {
        public event Action OnEscapePressed;

        private bool _inPauseMenu;

        protected override void Awake()
        {
            
        }

        protected override void Update()
        {
            if (_inPauseMenu == false)
                ControlledPawn?.OnInput(this);

            //Right alt for editor tests
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightAlt))
            {
                _inPauseMenu = !_inPauseMenu;

                OnEscapePressed?.Invoke();
            }
        }
    }
}