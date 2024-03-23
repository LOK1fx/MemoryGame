using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class LevelLoad : MonoBehaviour
    {
        [SerializeField] private List<string> _levelsToLoad;

        public void LoadLevelsOfList()
        {
            for (int i = 0; i < _levelsToLoad.Count; i++)
            {
                if (i == 0)
                {
                    LoadLevel(_levelsToLoad[i]);
                    continue;
                }

                LoadLevelAdditive(_levelsToLoad[i]);
            }
        }

        public void LoadLevel(string level)
        {
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void LoadLevelAdditive(string level)
        {
            SceneManager.LoadScene(level, LoadSceneMode.Additive);
        }
    }
}