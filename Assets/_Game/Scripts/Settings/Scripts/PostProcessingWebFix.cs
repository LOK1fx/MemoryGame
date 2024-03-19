using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LOK1game
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class PostProcessingWebFix : MonoBehaviour
    {
        private PostProcessVolume _volume;
        private AmbientOcclusion _ao;
        private AutoExposure _autoExposure;
        private ScreenSpaceReflections _srp;

        private void Start()
        {
            _volume = GetComponent<PostProcessVolume>();
            _volume.profile.TryGetSettings(out _ao);
            _volume.profile.TryGetSettings(out _autoExposure);
            _volume.profile.TryGetSettings(out _srp);

#if UNITY_WEBGL

            _ao?.enabled.Override(false);
            _autoExposure?.enabled.Override(false);
            _srp?.enabled.Override(false);

#else

            _ao?.enabled.Override(true);
            _autoExposure?.enabled.Override(true);
            _srp?.enabled.Override(true);

#endif
        }
    }
}