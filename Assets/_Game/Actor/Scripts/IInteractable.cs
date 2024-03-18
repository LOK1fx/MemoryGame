using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public interface IInteractable
    {
        void OnInteract(Player.Player sender);
    }
}