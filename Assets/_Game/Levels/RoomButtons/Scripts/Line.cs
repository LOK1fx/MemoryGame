using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LOK1game
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private float _timeDelay;

        private void Start()
        {
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(_timeDelay);
            Destroy(gameObject);
        }
    }
}