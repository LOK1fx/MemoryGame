using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Первое - то, что загрузится, \nвторое - что выгрузится. \nПроисходит одновременно. " +
            "\n\nВ листе указывать название сцен. \nНе забудь добавить эти сцены в BuildSettings.\n")]
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