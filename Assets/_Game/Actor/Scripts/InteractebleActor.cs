using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    public class InteractebleActor : MonoBehaviour, IInteractable
    {
        [Space]
        public UnityEvent OnInteractEvent;

        [Header("InteractionTooltip is deprecated. \nUse InteractionTooltipKey instead.")]
        [SerializeField] private string _interactionTooltip;
        [SerializeField] private string _interactionTooltipKey;

        public virtual void OnInteract(Player.Player sender)
        {
            OnInteractEvent?.Invoke();
        }

        public void OnHighlight(bool isActive)
        {
            
        }

        public string GetTooltip()
        {
            return LocalisationSystem.GetLocalisedValue(_interactionTooltipKey);
        }
    }
}

