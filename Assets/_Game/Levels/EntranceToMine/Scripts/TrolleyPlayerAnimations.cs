using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LOK1game
{
    [RequireComponent(typeof(Trolley))]
    public class TrolleyPlayerAnimations : MonoBehaviour
    {
        private const string TRIGG_ANIM_START_FORWARD = "StartForward";

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GameObject _playerRoot;

        private Trolley _trolley;

        private void Start()
        {
            _trolley = GetComponent<Trolley>();

            _trolley.OnStartMovement += OnTrolleyStartedMovement;
        }

        private void OnTrolleyStartedMovement()
        {
            _playerAnimator.SetTrigger(TRIGG_ANIM_START_FORWARD);
        }

        public void OnStartInteraction()
        {
            _playerRoot.SetActive(true);
            _virtualCamera.Priority = 500;
        }

        public void OnExitTrolley()
        {
            _virtualCamera.Priority = 0;
            _playerRoot.SetActive(false);
        }
    }
}