using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LOK1game
{
    public interface IPlayerHud
    {
        void Bind(Player.Player player, PlayerController controller);

        void EnableHiddenText(int idPhoto);
    }
}