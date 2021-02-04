using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UsefulPoolSystem.Core.Scripts
{
    [Serializable]
    public class UsefulSpawner<T> where T : UsefulObject
    {
        [SerializeField] private Transform parent;

        private List<T> _currentItems = new List<T>();

        public bool IsHaveAnyItem => _currentItems.Count > 0;

        public List<T> CurrentItems
        {
            get => _currentItems;
            private set => _currentItems = value;
        }

        public T SpawnNewItem()
        {
            var newItem = UsefulPoolObjectSystem.I.Spawn<T>();

            newItem.transform.SetParent(parent);

            if (CurrentItems.Contains(newItem))
                CurrentItems.Remove(newItem);
            
            CurrentItems.Add(newItem);

            return newItem;
        }

        public T SpawnNewItem(Vector3 position)
        {
            var newItem = SpawnNewItem();

            newItem.transform.position = position;

            return newItem;
        }

        public T SpawnNewItem(Vector2 position)
        {
            var newItem = SpawnNewItem();

            newItem.transform.position = position;

            return newItem;
        }

        public bool TryGetRandomItem(out T item)
        {
            item = null;

            if (!IsHaveAnyItem)
                return false;

            item = CurrentItems[Random.Range(0, CurrentItems.Count)];
            
            return true;
        }
        
        public void DeSpawnItem(T item)
        {
            if (!CurrentItems.Contains(item))
                return;

            item.DeSpawn();

            CurrentItems.Remove(item);
        }

        public void ClearAll()
        {
            if (!IsHaveAnyItem)
                return;
            
            CurrentItems.ToList().ForEach(DeSpawnItem);
        }

        private void UpdateItems()
        {
            CurrentItems.RemoveAll(x => !x.gameObject.activeSelf);
        }
    }
}