using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public interface ISaveable
    {
        ELevelName Level { get; }

        void Save();
        void Load();
    }
}