using UnityEngine;

namespace LOK1game.DebugTools
{
    public class DeveloperMenuEnabler : MonoBehaviour
    {
#if UNITY_EDITOR

        [Header("Works only if DevelopmentBuild \n" +
            "Option is enabled in BuildSettings")]
#endif

        [SerializeField] private bool _enableDevWindow;

        [Space]
        [SerializeField] private GameObject _devWindow;

#if UNITY_EDITOR

        private void Awake()
        {
            if (UnityEditor.EditorUserBuildSettings.development)
            {
                _devWindow.SetActive(_enableDevWindow);
            }
            else
            {
                _devWindow.SetActive(false);
            }
        }

#elif DEVELOPMENT_BUILD

        private void Awake()
        {
            _devWindow.SetActive(_enableDevWindow);
        }

#else

        private void Awake()
        {
            _devWindow.SetActive(false);
        }

#endif
    }
}