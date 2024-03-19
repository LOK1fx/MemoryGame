using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class LevelRestartButton : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}