using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class TriggerTrolleyBlocker : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<Trolley>(out var trolley))
            {
                trolley.TrolleyMovements.SetSpeed(0);
            }
        }
    }
}
