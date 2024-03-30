using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerController : Controller
    {
        public event Action OnEscapePressed;
        public event Action OnPhotosAlbumPressed;
        public event Action OnDocumentsPressed;

        public bool IsInputProcessing { get; set; } = true;

        protected override void Awake()
        {
            
        }

        protected override void Update()
        {
            if (IsInputProcessing)
                ControlledPawn?.OnInput(this);

            //Right alt for editor tests
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftAlt))
            {
                OnEscapePressed?.Invoke();
            }

            if (IsInputProcessing)
            {
                if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.H)) && IsInputProcessing == false)
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
}