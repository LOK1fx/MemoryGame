using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class TriggerCheckWeight : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Trolley>(out var trolley))
            {
                _animator.SetTrigger("Open");
                Debug.Log("awd");
            }
        }
    }
}
