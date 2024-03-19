using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace LOK1game
{
    public class Photocamera : MonoBehaviour
    {
        [SerializeField] private Camera _defaultCamera;
        [SerializeField] private Camera _photoCamera;
        [SerializeField] private Light _flashlight;
        [SerializeField] private LayerMask _interestPointMask;
        [SerializeField] private float _interestPointMaxDistance = 100f;

        [Space]
        [SerializeField] private float _flashlightShowTime = 0.2f;

        [Space]
        [SerializeField] private int _photoWidth;
        [SerializeField] private int _photoHeigth;

        public void TakePhoto(out Texture2D photo, out PointOfInterest pointOfInterest)
        {
            _defaultCamera.gameObject.SetActive(false);
            _photoCamera.gameObject.SetActive(true);

            StartCoroutine(FlashingRoutine());

            var targetTexture = _photoCamera.targetTexture;

            var currentRenderTexture = _photoCamera.targetTexture;
            RenderTexture.active = _photoCamera.targetTexture;

            _photoCamera.Render();

            var texture = new Texture2D(_photoWidth, _photoHeigth);
            texture.ReadPixels(new Rect(0, 0, _photoWidth, _photoHeigth), 0, 0);
            texture.Apply();
            RenderTexture.active = currentRenderTexture;

            photo = texture;

            var bytes = texture.EncodeToPNG();
            //Destroy(texture);

            File.WriteAllBytes(Application.dataPath + "/PhotosTaken/" + "photo01" + ".png", bytes);

            _defaultCamera.gameObject.SetActive(true);
            _photoCamera.gameObject.SetActive(false);

            TryGetInterestPoint(out pointOfInterest);
        }

        private bool TryGetInterestPoint(out PointOfInterest pointOfInterest)
        {
            if(Physics.Raycast(_defaultCamera.transform.position, _defaultCamera.transform.forward,
                out var hit, _interestPointMaxDistance, _interestPointMask, QueryTriggerInteraction.Collide))
            {
                return hit.collider.gameObject.TryGetComponent<PointOfInterest>(out pointOfInterest);
            }

            pointOfInterest = null;

            return false;
        }

        private IEnumerator FlashingRoutine()
        {
            _flashlight.gameObject.SetActive(true);

            yield return new WaitForSeconds(_flashlightShowTime);

            _flashlight.gameObject.SetActive(false);
        }
    }
}