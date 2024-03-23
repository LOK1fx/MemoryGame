using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game
{
    public class Trolley : MonoBehaviour, IInteractable
    {
        [SerializeField] private TrolleyPlayerAnimations _interactionAnimations;

        public void OnHighlight(bool isActive)
        {
            
        }

        public void OnInteract(Player.Player sender)
        {
            _interactionAnimations.OnStartInteraction();

            sender.Movement.StopMovementInput();
            sender.Movement.SetAxisInput(Vector2.zero);
        }

        public string GetTooltip()
        {
            return "Press F to sit in the trolley";
        }
    }
}