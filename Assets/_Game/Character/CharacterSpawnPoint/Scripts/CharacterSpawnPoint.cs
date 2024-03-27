using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    public class CharacterSpawnPoint : SaveableActorBase
    {
        public Vector3 Position => transform.position;
        public bool AllowNeutral => _allowNeutral;
        public bool AllowPlayer => _allowPlayer;
        public bool AllowEnemy => _allowEnemy;
        public bool AllowNPC => _allowNPC;
        public bool AllowSpectator => _allowSpectator;

        [Header("Flags")]
        [SerializeField] private bool _allowNeutral;
        [SerializeField] private bool _allowPlayer;
        [SerializeField] private bool _allowEnemy;
        [SerializeField] private bool _allowNPC;
        [SerializeField] private bool _allowSpectator;
        
        private List<Actor> _spawnedAtPoint = new List<Actor>();

        public Actor SpawnActor(Actor actor)
        {
            var newActor = Instantiate(actor, transform.position, transform.rotation);

            _spawnedAtPoint.Add(newActor);

            return newActor;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
            
#if UNITY_EDITOR
            
            Handles.Label(transform.position + Vector3.up * 0.5f, $"S:{name}");
            
#endif
        }

        public override void Load()
        {
            transform.localPosition = PlayerPrefsExtensions.GetVector3($"{(int)Level}_Spawnpoint");
        }

        public override void Save()
        {
            PlayerPrefsExtensions.SetVector3($"{(int)Level}_Spawnpoint", transform.localPosition);
        }
    }
}