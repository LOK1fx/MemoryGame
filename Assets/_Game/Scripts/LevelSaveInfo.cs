using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [System.Serializable]
    public class LevelSaveInfo
    {
        public List<ISaveable> Saveables { get; private set; }
    }
}