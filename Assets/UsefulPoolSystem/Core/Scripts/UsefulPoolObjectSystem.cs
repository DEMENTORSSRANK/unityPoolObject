using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UsefulPoolSystem.Core.Scripts
{
    public class UsefulPoolObjectSystem : MonoBehaviour
    {
        [SerializeField] private UsefulPool[] _pools;
        
        public static UsefulPoolObjectSystem I { get; private set; }

        public T Spawn<T>() where T : UsefulObject
        {
            var toSpawnTypeName = typeof(T).Name;

            if (_pools.All(x => x.TypeName != toSpawnTypeName))
            {
                throw new Exception($"Wrong {nameof(T)} to spawn (no prefab in queue for spawn");
            }

            var foundedPool = _pools.First(x => x.TypeName == toSpawnTypeName);

            var spawned = foundedPool.SpawnNewObject();
            
            spawned.gameObject.SetActive(true);

            spawned.OnSpawned();
            
            return (T) spawned;
        }

        public void DeSpawn(UsefulObject pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            
            pooledObject.OnDeSpawned();
        }
        
        private void CreateStartQueues()
        {
            _pools.ToList().ForEach(x => x.InitQueue());
        }

        private void Awake()
        {
            I = this;
            
            CreateStartQueues();
        }

        [Serializable]
        private class UsefulPool
        {
            [SerializeField] private UsefulObject _prefabPooled;

            [SerializeField] private Transform _parent;

            [SerializeField] private int _maxCount = 16;

            private Queue<UsefulObject> _queue;

            public UsefulObject PrefabPooled => _prefabPooled;
            
            public string TypeName { get; private set; }

            public UsefulObject SpawnNewObject()
            {
                var newObject = _queue.Dequeue();
                
                if (newObject.gameObject.activeSelf)
                    newObject.OnDeSpawned();

                _queue.Enqueue(newObject);

                return newObject;
            }

            public void InitQueue()
            {
                var typeHave = _prefabPooled.GetType();
                
                TypeName = typeHave.Name;

                _queue = new Queue<UsefulObject>();

                for (var i = 0; i < _maxCount; i++)
                {
                    AddElementToQueue();
                }
            }

            private void AddElementToQueue()
            {
                var createdElement = Instantiate(_prefabPooled, _parent);
                
                createdElement.gameObject.SetActive(false);
                
                _queue.Enqueue(createdElement);
            }
        }
    }
}