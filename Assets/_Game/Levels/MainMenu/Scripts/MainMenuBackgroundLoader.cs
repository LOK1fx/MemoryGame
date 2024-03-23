using UnityEngine;
using UnityEngine.SceneManagement;

public enum ELevelName
{
    None = -1,
    WakeUp_01,
    RoomsButton,
    Labirint01_03,
}

namespace LOK1game
{
    public class MainMenuBackgroundLoader : MonoBehaviour
    {
        [SerializeField] private ELevelName _defaultBackground;

        [Space]
        [SerializeField] private GameObject _defaultLevel;
        [SerializeField] private GameObject _wakeupLevel;
        [SerializeField] private GameObject _roomButtonLevel;
        [SerializeField] private GameObject _labirintLevel;

        private void Start()
        {
            if (MenuBackgroundRemember.Instance != null)
                Load(MenuBackgroundRemember.Instance.GetLevel());
            else
                Load(_defaultBackground);
        }

        private void Load(ELevelName level)
        {
            switch (level)
            {
                case ELevelName.None:
                    Load(_defaultBackground);
                    break;
                case ELevelName.WakeUp_01:
                    SceneManager.LoadScene("WakeUp_01", LoadSceneMode.Additive);
                    break;
                case ELevelName.RoomsButton:
                    SceneManager.LoadScene("RoomsButton_ThePast", LoadSceneMode.Additive);
                    break;
                case ELevelName.Labirint01_03:
                    SceneManager.LoadScene("Labirint01_03", LoadSceneMode.Additive);
                    break;
                default:
                    break;
            }

            CorrectCamera(level);
        }

        private void CorrectCamera(ELevelName level)
        {
            switch (level)
            {
                case ELevelName.None:
                    _defaultLevel.SetActive(true);
                    break;
                case ELevelName.WakeUp_01:
                    _wakeupLevel.SetActive(true);
                    break;
                case ELevelName.RoomsButton:
                    _roomButtonLevel.SetActive(true);
                    break;
                case ELevelName.Labirint01_03:
                    _labirintLevel.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}