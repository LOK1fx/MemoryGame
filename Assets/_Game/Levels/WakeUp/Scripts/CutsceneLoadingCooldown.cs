using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class CutsceneLoadingCooldown : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _director;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "WakeUp_01")
                _director.Play();
        }
    }
}