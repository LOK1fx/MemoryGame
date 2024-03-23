using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game
{
    public class Trolley : MonoBehaviour, IInteractable
    {
        public void OnHighlight(bool isActive)
        {
            throw new System.NotImplementedException();
        }

        public void OnInteract(Player.Player sender)
        {
            throw new System.NotImplementedException();
        }

        public string GetTooltip()
        {
            return "Press F to sit in the trolley";
        }
    }
}