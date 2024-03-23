using UnityEngine;

namespace LOK1game
{
    public class Labirint01SlidingSoundTrigger : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private bool _isThatEntrance;

        private bool _isActivated = true;

        private void OnTriggerEnter(Collider other)
        {
            if (_isThatEntrance == false || _isActivated == false)
                return;

            if (other.TryGetComponent<Player.Player>(out var player))
            {
                var tail = player.gameObject.AddComponent<SlidingSoundTail>();

                tail.Initialize(_clip);

                _isActivated = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_isThatEntrance == true || _isActivated == false)
                return;

            if (other.TryGetComponent<Player.Player>(out var player)
                && other.TryGetComponent<SlidingSoundTail>(out var tail))
            {
                Destroy(tail);

                _isActivated = false;
            }
        }
    }
}