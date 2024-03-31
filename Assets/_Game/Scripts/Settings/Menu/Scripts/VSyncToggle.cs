using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LOK1game.Tools;

namespace LOK1game.UI
{
    [RequireComponent(typeof(Toggle))]
    public class VSyncToggle : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            if (QualitySettings.vSyncCount > 0)
                _toggle.isOn = true;
            else
                _toggle.isOn = false;
        }

        public void SetVSync(bool active)
        {
            if (active)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }
    }
}