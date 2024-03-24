using UnityEngine;

namespace LOK1game
{
    public class PlayerTrolleyDepasserTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Trolley>(out var trolley))
            {
                if(trolley.PlayerInTrolley != null)
                {
                    trolley.DepassPlayerFromTrolley(trolley.PlayerInTrolley);
                }
            }
        }
    }
}