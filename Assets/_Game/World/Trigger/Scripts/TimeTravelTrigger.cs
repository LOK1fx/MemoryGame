using UnityEngine;

namespace LOK1game
{
    public class TimeTravelTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Eyeblink>(out var eyeblink))
            {
                eyeblink.Blink();
            }
        }
    }
}