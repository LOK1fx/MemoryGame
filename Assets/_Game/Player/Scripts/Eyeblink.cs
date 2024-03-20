using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class Eyeblink : MonoBehaviour
    {
        private const string TRIG_BLINK = "Blink";

        [SerializeField] private Animator _animator;

        public void Blink()
        {
            _animator.SetTrigger(TRIG_BLINK);
        }
    }
}