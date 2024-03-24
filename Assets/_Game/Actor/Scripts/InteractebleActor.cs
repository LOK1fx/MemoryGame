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

        public void OnInteract(Player.Player sender)
        {
            OnInteractEvent?.Invoke();
            sender.GetComponent<Eyeblink>().Blink();
        }

        public void OnHighlight(bool isActive)
        {
            
        }

        public string GetTooltip()
        {
            return "Press F";
        }
    }
}

