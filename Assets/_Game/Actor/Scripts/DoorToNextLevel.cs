using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK1game
{
    public class DoorToNextLevel : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _nextLevelName;

        private bool _isActive = true;

        public void OnHighlight(bool isActive)
        {
            
        }

        public void OnInteract(Player.Player sender)
        {
            if (_isActive == false)
                return;

            _isActive = true;

            StartCoroutine(LoadNextLevel());

            sender.Camera.UnlockCursor();
            sender.Movement.Rigidbody.isKinematic = true;
        }

        private IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(1f);

            SceneManager.LoadSceneAsync(_nextLevelName, LoadSceneMode.Single);
        }

        public string GetTooltip()
        {
            return "Press F to procced to next area";
        }
    }
}