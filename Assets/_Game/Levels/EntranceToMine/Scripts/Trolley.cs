using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game
{
    public class Trolley : MonoBehaviour, IInteractable
    {
        public TrolleyMovements TrolleyMovements => _trolleyMovements;

        [SerializeField] private TrolleyMovements _trolleyMovements;

        [SerializeField] private TrolleyPlayerAnimations _interactionAnimations;
        [SerializeField] private Transform _actualPlayerPositionInTransport;
        [SerializeField] private Transform _playerBackPositionTransform;

        [Space]
        [SerializeField] private float _maxHorizontalPlayerCameraViewAngle = 30f;
        [SerializeField] private float _maxVerticalPlayerCameraViewAngle = 50f;

        private Player.Player _playerInTrolley; 

        public void OnHighlight(bool isActive)
        {
            
        }

        public void OnInteract(Player.Player sender)
        {
            if (sender.State.InTransport == false)
            {
                _interactionAnimations.OnStartInteraction();

                sender.PassIntroTransport();
                sender.HideVisuals();

                sender.ItemManager.StopInput(sender);

                sender.transform.SetParent(_actualPlayerPositionInTransport);
                sender.transform.localPosition = Vector3.zero;
                sender.Movement.Rigidbody.position = _actualPlayerPositionInTransport.position;

                ReparentedScalesCorrect(sender);

                sender.Camera.LimitViewAngles(_maxHorizontalPlayerCameraViewAngle, _maxVerticalPlayerCameraViewAngle);
                _playerInTrolley = sender;
            }
            else
            {
                _interactionAnimations.OnExitTrolley();

                sender.DepassFromTransport();
                sender.ShowVisuals();

                sender.ItemManager.StartInput(sender);

                sender.transform.SetParent(null);
                sender.transform.position = _playerBackPositionTransform.position;
                sender.Movement.Rigidbody.position = _playerBackPositionTransform.position;

                ReparentedScalesCorrect(sender);

                sender.Camera.SetDefaultViewAngles();
                _playerInTrolley = null;
            }
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