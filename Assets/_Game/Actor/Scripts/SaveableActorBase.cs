using UnityEngine;

namespace LOK1game
{
    public abstract class SaveableActorBase : MonoBehaviour, ISaveable
    {
        public ELevelName Level => level;

        [SerializeField] protected ELevelName level;

        public abstract void Load();
        public abstract void Save();
    }
}