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

        [Space]
        [SerializeField] private int _photoWidth;
        [SerializeField] private int _photoHeigth;

        public void TakePhoto(out Texture2D photo)
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
        }

        private IEnumerator FlashingRoutine()
        {
            _flashlight.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            _flashlight.gameObject.SetActive(false);
        }
    }
}