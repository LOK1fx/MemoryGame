using UnityEngine;

namespace LOK1game.DebugTools
{ 
    public class DebugFreecam : MonoBehaviour
    {
        public LayerMask DebugLayerMask;

        private LayerMask _defaultLayerMask;

        public GameObject StandardLight;
        public GameObject StrongLight;

        private int _currentLightMode = 0;

        public float movementSpeed = 10f;
        public float fastMovementSpeed = 100f;
        public float freeLookSensitivity = 3f;
        public float zoomSensitivity = 10f;
        public float fastZoomSensitivity = 50f;

        private bool _looking = false;
        private bool _isDebugLayerActive;

        private void Start()
        {
            _defaultLayerMask = Camera.main.cullingMask;
        }

        private void OnDestroy()
        {
            if (Camera.main != null)
                Camera.main.cullingMask = _defaultLayerMask;  
        }

        private void Update()
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.C))
            {
                SwitchDebugViewMode();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                SwitchLightMode();
            }
        }

        public void SwitchDebugViewMode()
        {
            if (_isDebugLayerActive)
            {
                Camera.main.cullingMask = _defaultLayerMask;

                _isDebugLayerActive = false;
            }
            else
            {
                Camera.main.cullingMask = DebugLayerMask;

                _isDebugLayerActive = true;
            }
        }

        private void Movement()
        {
            var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
            {
                transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
            {
                transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
            }

            if (_looking)
            {
                float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
                float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
                transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
            }

            float axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0)
            {
                var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
                transform.position = transform.position + transform.forward * axis * zoomSensitivity;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartLooking();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                StopLooking();
            }
        }

        private void OnDisable()
        {
            StopLooking();
        }

        public void SwitchLightMode()
        {
            if ((_currentLightMode % 3) == 0)
            {
                StandardLight.SetActive(false);
                StrongLight.SetActive(false);
            }
            else if ((_currentLightMode % 3) == 1)
            {
                StandardLight.SetActive(true);
                StrongLight.SetActive(false);
            }
            else if ((_currentLightMode % 3) == 2)
            {
                StandardLight.SetActive(false);
                StrongLight.SetActive(true);
            }

            _currentLightMode++;
        }

        public void StartLooking()
        {
            _looking = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void StopLooking()
        {
            _looking = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}