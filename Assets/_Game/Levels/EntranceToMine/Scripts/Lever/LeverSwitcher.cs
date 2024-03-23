using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    [RequireComponent(typeof(Outline))]
    public class LeverSwitcher : MonoBehaviour, IInteractable
    {
        [SerializeField] private Lever _lever;
        [SerializeField] private ForkPointPath _forkPointPath;
        private Outline _outline;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public string GetTooltip()
        {
            return "Press F to switch";
        }

        public void OnHighlight(bool isActive)
        {
            _outline.enabled = isActive;
        }

        public void OnInteract(Player.Player sender)
        {
            switch (_forkPointPath.SwitcherPosition)
            {
                case ESwitcherPosition.Neutral:
                    _forkPointPath.SwitcherPosition = ESwitcherPosition.Straight;
                    _forkPointPath.SetStraightPath();
                    break;
                case ESwitcherPosition.Straight:
                    _forkPointPath.SwitcherPosition = ESwitcherPosition.Other;
                    _forkPointPath.SetOtherPath();
                    break;
                case ESwitcherPosition.Other:
                    _forkPointPath.SwitcherPosition = ESwitcherPosition.Neutral;
                    _forkPointPath.SetNeutralPath();
                    break;
                default:
                    break;
            }
            _lever.LeverAnimation.PlaySwitchAnimation(_forkPointPath.SwitcherPosition);
        }
    }
}
