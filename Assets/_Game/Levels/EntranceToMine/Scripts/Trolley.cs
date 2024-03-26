using System;
using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game
{
    public class Trolley : MonoBehaviour, IInteractable
    {
        public event Action OnStartMovement;

        public TrolleyMovements TrolleyMovements => _trolleyMovements;
        public Player.Player PlayerInTrolley => _playerInTrolley;

        [SerializeField] private TrolleyMovements _trolleyMovements;

        [SerializeField] private TrolleyPlayerAnimations _interactionAnimations;
        [SerializeField] private Transform _actualPlayerPositionInTransport;
        [SerializeField] private Transform _playerBackPositionTransform;

        [Space]
        [SerializeField] private float _maxHorizontalPlayerCameraViewAngle = 30f;
        [SerializeField] private float _maxVerticalPlayerCameraViewAngle = 50f;

        [SerializeField] private AudioSource _audioSource;

        private Player.Player _playerInTrolley;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && _playerInTrolley != null)
            {
                _trolleyMovements.StartTrolley();

                OnStartMovement?.Invoke();
            }

            if (_trolleyMovements.CurrentSpeed > 0)
            {
                _audioSource.gameObject.SetActive(true);
            }
            else
            {
                _audioSource.gameObject.SetActive(false);
            }
        }

        public void OnHighlight(bool isActive)
        {
            
        }

        public void OnInteract(Player.Player sender)
        {
            if (sender.State.InTransport == false)
            {
                PassPlayerIntoTrolley(sender);
            }
            else
            {
                DepassPlayerFromTrolley(sender);
            }
        }

        public void PassPlayerIntoTrolley(Player.Player player)
        {
            if (player.State.InTransport == true)
                return;

            _interactionAnimations.OnStartInteraction();

            player.PassIntroTransport();
            player.HideVisuals();

            player.ItemManager.StopInput(player);

            player.transform.SetParent(_actualPlayerPositionInTransport);
            player.transform.localPosition = Vector3.zero;
            player.Movement.Rigidbody.position = _actualPlayerPositionInTransport.position;

            ReparentedScalesCorrect(player);

            player.Camera.LimitViewAngles(_maxHorizontalPlayerCameraViewAngle, _maxVerticalPlayerCameraViewAngle);
            _playerInTrolley = player;
        }

        public void DepassPlayerFromTrolley(Player.Player player)
        {
            if (player.State.InTransport == false)
                return;

            if (_playerInTrolley == null)
                return;

            _interactionAnimations.OnExitTrolley();

            player.DepassFromTransport();
            player.ShowVisuals();

            player.ItemManager.StartInput(player);

            player.transform.SetParent(null);
            player.transform.position = _playerBackPositionTransform.position;
            player.Movement.Rigidbody.position = _playerBackPositionTransform.position;

            ReparentedScalesCorrect(player);

            player.Camera.SetDefaultViewAngles();
            _playerInTrolley = null;
        }

        private void ReparentedScalesCorrect(Player.Player sender)
        {
            sender.transform.localScale = Vector3.one;
            sender.transform.localRotation = Quaternion.identity;
        }

        public string GetTooltip()
        {
            return "Press F to sit in the trolley";
        }

        private void OnDrawGizmos()
        {
            if (_actualPlayerPositionInTransport == null || _playerBackPositionTransform == null)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(_actualPlayerPositionInTransport.position, _actualPlayerPositionInTransport.up * 2f);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_playerBackPositionTransform.position, _playerBackPositionTransform.up * 2f);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.TryGetComponent<KillZone>(out var killZone) && _playerInTrolley != null)
            {
                _playerInTrolley.TakeDamage(new Damage(100));
            }
        }
    }
}