using System;
using UnityEngine;
using UsefulPoolSystem.Core.Scripts;
using Random = UnityEngine.Random;

namespace UsefulPoolSystem.Example.Physic_Objects.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicPrefab : UsefulObject
    {
        [SerializeField] private float forceGive = 30;
        
        private Rigidbody _rigidBody;
        
        public void RandomAddForceUp(float force)
        {
            var randomDirection = Vector3.up;

            randomDirection.x = Random.Range(-1f, 1f);

            randomDirection.z = Random.Range(-1f, 1f);
            
            _rigidBody.AddForce(randomDirection * force, ForceMode.Impulse);
        }

        public override void OnSpawned()
        {
            RandomAddForceUp(forceGive);
        }

        public override void OnDeSpawned()
        {
            _rigidBody.Sleep();
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
    }
}