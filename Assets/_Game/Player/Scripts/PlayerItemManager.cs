using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LOK1game
{
    public class PlayerItemManager : MonoBehaviour, IInputabe
    {
        public event Action OnStartCameraAim;
        public event Action OnStopCameraAim;
        public event Action<PhotoConfig> OnPhotoTaken;

        [SerializeField] private Photocamera _photocamera;

        private Player.Player _player;

        private bool _isAiming;

        public void Construct(Player.Player player)
        {
            _player = player;
        }

        public void OnInput(object sender)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _isAiming = true;
                OnStartCameraAim?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                _isAiming = false;
                OnStopCameraAim?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _isAiming)
            {
                TakePhoto();
            }
        }

        private void TakePhoto()
        {
            _photocamera.TakePhoto(out var photo, out var pointOfInterest);

            var description = "";
            var typePhoto = ETypePhoto.Simple;
            var idPhoto = 0;
            var hiddenText = "";

            if (pointOfInterest != null)
            {
                description = pointOfInterest.Description;
                typePhoto = pointOfInterest.TypePhoto;
                idPhoto = pointOfInterest.IdPhoto;
                hiddenText = pointOfInterest.HiddenText;
            }

            OnPhotoTaken?.Invoke(new PhotoConfig(typePhoto, idPhoto, photo, description, hiddenText));
        }
    }
}