using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LOK1game
{
    public class InteractionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textInteracrion;

        public void DisplayTextInteracrion(bool isActive)
        {
            _textInteracrion.gameObject.SetActive(isActive);
        }
    }
}
