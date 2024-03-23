using UnityEngine;

namespace LOK1game
{
    public class MenuBackgroundRemember : MonoBehaviour
    {
        public static MenuBackgroundRemember Instance { get; private set; }

        private ELevelName _currentRemembered = ELevelName.None;

        private void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void Remember(ELevelName level)
        {
            _currentRemembered = level;
        }

        public ELevelName GetLevel()
        {
            return _currentRemembered;
        }
    }
}