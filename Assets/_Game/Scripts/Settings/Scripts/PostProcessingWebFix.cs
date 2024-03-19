using System.Collections;
using System.Collections.Generic;
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

        private void Start()
        {
            _volume = GetComponent<PostProcessVolume>();
            _volume.profile.TryGetSettings(out _ao);
            _volume.profile.TryGetSettings(out _autoExposure);

#if UNITY_WEBGL

            _ao?.enabled.Override(false);
            _autoExposure?.enabled.Override(false);

#else

            _ao?.enabled.Override(true);
            _autoExposure?.enabled.Override(true);

#endif
        }
    }
}