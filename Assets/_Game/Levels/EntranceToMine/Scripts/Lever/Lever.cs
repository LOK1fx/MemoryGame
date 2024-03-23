using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class Lever : MonoBehaviour
    {
        public LeverAnimation LeverAnimation => _leverAnimation;

        public LeverSwitcher LeverSwitcher => _leverSwitcher;

        [SerializeField] private LeverAnimation _leverAnimation;
        [SerializeField] private LeverSwitcher _leverSwitcher;
    }
}
