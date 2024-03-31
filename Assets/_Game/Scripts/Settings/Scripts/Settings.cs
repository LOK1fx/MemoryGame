using System;
using UnityEngine;

namespace LOK1game
{
    public static class Settings
    {
        public static event Action<float> OnSensivityChanged;

        private const string PLAYER_SETTING_PREFIX = "PlayerSettings_";
        private const string SENSIVITY = PLAYER_SETTING_PREFIX + "Sensivity";

        public static float GetSensivity()
        {
            var sens = PlayerPrefs.GetFloat(SENSIVITY);

            if (sens == 0)
            {
                return 5f; //default value
            }
            else
            {
                return sens;
            }
        }

        public static void SetSensivity(float value)
        {
            PlayerPrefs.SetFloat(SENSIVITY, value);

            OnSensivityChanged?.Invoke(value);
        }
    }
}