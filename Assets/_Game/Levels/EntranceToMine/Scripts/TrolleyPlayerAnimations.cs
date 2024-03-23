using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LOK1game
{
    public class TrolleyPlayerAnimations : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GameObject _playerRoot;

        public void OnStartInteraction()
        {
            _playerRoot.SetActive(true);
            _virtualCamera.Priority = 500;
        }
    }
}