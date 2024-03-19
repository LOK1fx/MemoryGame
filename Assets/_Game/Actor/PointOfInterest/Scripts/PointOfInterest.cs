using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class PointOfInterest : MonoBehaviour
    {
        public string Note => _note;

        [SerializeField][TextArea] private string _note;
    }
}