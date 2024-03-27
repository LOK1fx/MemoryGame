using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools
{
    public static class PlayerPrefsExtensions
    {
        public static void SetVector3(string key, Vector3 vector)
        {
            PlayerPrefs.SetFloat($"{key}_x", vector.x);
            PlayerPrefs.SetFloat($"{key}_y", vector.y);
            PlayerPrefs.SetFloat($"{key}_z", vector.z);
        }

        public static Vector3 GetVector3(string key)
        {
            var vector = new Vector3(GetAxis(key, "x"), GetAxis(key, "y"), GetAxis(key, "z"));

            return vector;
        }

        private static float GetAxis(string key, string axis)
        {
            return PlayerPrefs.GetFloat($"{key}_{axis}");
        }
    }
}