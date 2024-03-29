using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using TMPro;

namespace LOK1game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class VersionTMPText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();

            _text.text = $"{LocalisationSystem.GetLocalisedValue("text_version")}: {Application.version}";
        }
    }
}