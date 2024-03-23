using UnityEngine;

namespace LOK1game
{
    public class RememberLevel : MonoBehaviour
    {
        [SerializeField] private ELevelName _level;

        public void Remember()
        {
            if (MenuBackgroundRemember.Instance != null)
                MenuBackgroundRemember.Instance.Remember(_level);
        }
    }
}