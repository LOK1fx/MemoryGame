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

        [SerializeField] private string _interactionTooltip;

        public virtual void OnInteract(Player.Player sender)
        {
            OnInteractEvent?.Invoke();
        }

        public void OnHighlight(bool isActive)
        {
            
        }

        public string GetTooltip()
        {
            return _interactionTooltip;
        }
    }
}

