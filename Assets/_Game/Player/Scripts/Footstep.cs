using UnityEngine;

namespace LOK1game.Character.Generic
{
    [RequireComponent(typeof(AudioSource))]
    public class Footstep : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 0.6f;

        private AudioSource _source;
        private FootstepCollectionData _data;

        public void Construct(FootstepCollectionData data)
        {
            _data = data;
        }

        public void PlaySound()
        {
            var clip = _data.FootstepsClips[Random.Range(0, _data.FootstepsClips.Count)];

            _source.PlayOneShot(clip);
        }

        public void PlayJump()
        {
            _source.PlayOneShot(_data.JumpClip);
        }

        public void PlayLand()
        {
            _source.PlayOneShot(_data.LandClip);
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }
    }
}