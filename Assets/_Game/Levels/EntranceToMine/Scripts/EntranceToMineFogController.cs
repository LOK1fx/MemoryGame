using UnityEngine;

namespace LOK1game
{
    public class EntranceToMineFogController : MonoBehaviour
    {
        [SerializeField] private float _fogTransitionSpeed;

        [Space]
        [SerializeField] private Color _mineFogColor;
        [SerializeField] private float _fogDensityInMine;

        private float _defaultFogDensity;
        private Color _defaultFogColor;

        private float _targetFogDensity;

        private void Start()
        {
            _defaultFogDensity = RenderSettings.fogDensity;
            _defaultFogColor = RenderSettings.fogColor;

            _targetFogDensity = _defaultFogDensity;
        }

        private void Update()
        {
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, _targetFogDensity,
                _fogTransitionSpeed * Time.deltaTime);
        }


        public void SetDefaultFog()
        {
            _targetFogDensity = _defaultFogDensity;

            RenderSettings.fogColor = _defaultFogColor;
        }

        public void SetMineFog()
        {
            _targetFogDensity = _fogDensityInMine;

            RenderSettings.fogColor = _mineFogColor;
        }
    }
}