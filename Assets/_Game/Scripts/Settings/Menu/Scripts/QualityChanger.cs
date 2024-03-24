using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace LOK1game
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class QualityChanger : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();

#if UNITY_WEBGL

            var options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("Very Low"),
                new TMP_Dropdown.OptionData("Low"),
                new TMP_Dropdown.OptionData("Medium")
            };

            _dropdown.AddOptions(options);

#else

            var options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("Very Low"),
                new TMP_Dropdown.OptionData("Low"),
                new TMP_Dropdown.OptionData("Medium"),
                new TMP_Dropdown.OptionData("High"),
                new TMP_Dropdown.OptionData("Very High"),
                new TMP_Dropdown.OptionData("Ultra")
            };

            _dropdown.AddOptions(options);

#endif

            _dropdown.value = QualitySettings.GetQualityLevel();
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
    }
}