using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class PointOfInterest : MonoBehaviour
    {
        public ETypePhoto TypePhoto => _typePhoto;
        public string Description => _description;
        public string HiddenText => _hiddenText;
        public int IdPhoto => _idPhoto;

        [SerializeField] private ETypePhoto _typePhoto;
        [SerializeField][TextArea] private string _description;
        [SerializeField][TextArea] private string _hiddenText;
        [SerializeField] private int _idPhoto;
    }
}