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
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_very_low")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_low")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_medium"))
            };

            _dropdown.AddOptions(options);

#else

            var options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_very_low")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_low")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_medium")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_high")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_very_high")),
                new TMP_Dropdown.OptionData(LocalisationSystem.GetLocalisedValue("settings_graphics_level_ultra"))
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