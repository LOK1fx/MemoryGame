using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeverAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlaySwitchAnimation(ESwitcherPosition switcherPosition)
    {
        _animator.SetInteger("Switch", (int)switcherPosition);
    }
}
