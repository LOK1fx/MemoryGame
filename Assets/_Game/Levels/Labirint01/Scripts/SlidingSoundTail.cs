using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(AudioSource))]
    public class SlidingSoundTail : MonoBehaviour
    {
        private AudioClip _clip;
        private AudioSource _source;

        public void Initialize(AudioClip clip)
        {
            _clip = clip;

            _source = GetComponent<AudioSource>();

            _source.PlayOneShot(_clip);
        }
    }
}