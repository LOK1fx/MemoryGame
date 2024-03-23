using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class InstaLevelLoader : MonoBehaviour
    {
        [SerializeField] private List<string> _levelsToLoad;

        private void Awake()
        {
            foreach (var level in _levelsToLoad)
            {
                SceneManager.LoadScene(level, LoadSceneMode.Additive);
            }
        }
    }
}