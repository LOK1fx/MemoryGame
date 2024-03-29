using UnityEngine;
using UnityEngine.UI;

namespace LOK1game.UI
{
    public class UILocalizeButton : MonoBehaviour
    {
        [SerializeField] private LocalisationSystem.ELanguage _language;

        public void ChangeLanguage()
        {
            LocalisationSystem.Language = _language;
        }
    }
}