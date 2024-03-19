using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("������ - ��, ��� ����������, \n������ - ��� ����������. \n���������� ������������. " +
            "\n\n� ����� ��������� �������� ����. \n�� ������ �������� ��� ����� � BuildSettings.\n")]
        [SerializeField] private List<string> _levelsToLoad;
        [SerializeField] private List<string> _levelsToUnload;

        public void Load()
        {
            foreach (var level in _levelsToLoad)
            {
                SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
            }
            foreach (var level in _levelsToUnload)
            {
                SceneManager.UnloadSceneAsync(level);
            }
        }
    }
}