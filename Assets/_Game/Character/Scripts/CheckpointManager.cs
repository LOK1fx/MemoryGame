using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;

            DontDestroyOnLoad(Instance);
        }
    }
}