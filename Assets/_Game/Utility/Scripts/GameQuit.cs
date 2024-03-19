using UnityEngine;

namespace LOK1game.Utils
{
    public class GameQuit : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void Quit(int exitCode)
        {
            Application.Quit(exitCode);
        }
    }
}

