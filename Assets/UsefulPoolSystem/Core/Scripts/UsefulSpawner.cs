using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace UsefulPoolSystem.Core.Scripts
{
    [Serializable]
    public class UsefulSpawner<T> where T : UsefulObject
    {
        [SerializeField] private Transform _parent;

        [SerializeField] private ForeverSpawner _foreverSpawner;

        private List<T> _currentItems = new List<T>();

        private Coroutine _foreverSpawning;

        public bool IsHaveAnyItem => _currentItems.Count > 0;

        public ForeverSpawner ForeverSpawnParameters => _foreverSpawner;

        public UnityAction<T> OnForeverSpawned { get; set; }

        public List<T> CurrentItems
        {
            get => _currentItems;
            private set => _currentItems = value;
        }

        public T SpawnNewItem()
        {
            var newItem = UsefulPoolObjectSystem.I.Spawn<T>();

            newItem.transform.SetParent(_parent);

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

        public void StartForeverSpawning(MonoBehaviour invoker)
        {
            StopForeverSpawning(invoker);

            _foreverSpawning = invoker.StartCoroutine(ForeverSpawning());
        }

        public void StopForeverSpawning(MonoBehaviour invoker)
        {
            if (_foreverSpawning == null)
                return;

            invoker.StopCoroutine(_foreverSpawning);

            _foreverSpawning = null;
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

        private IEnumerator ForeverSpawning()
        {
            void Spawn()
            {
                var item = SpawnNewItem();

                OnForeverSpawned?.Invoke(item);
            }

            if (_foreverSpawner.SpawnWhenBegin)
                Spawn();

            while (true)
            {
                yield return new WaitForSeconds(_foreverSpawner.RandomDelay);

                Spawn();
            }
        }

        [Serializable]
        public class ForeverSpawner
        {
            [SerializeField] private float _minDelay = .2f;

            [SerializeField] private float _maxDelay = .5f;

            [Tooltip("If it is, then spawn on StartForeverSpawning method (not in start game)")]
            [Space(5)]
            [SerializeField]
            private bool _spawnWhenBegin;

            [Header("Range delays (\"Max\" == 0 == Infinity)")] [SerializeField]
            private Range _minRange;

            [SerializeField] private Range _maxRange;


            public float MinDelay
            {
                get => _minDelay;
                set
                {
                    value = _minRange.GetClamped(value);
                    
                    value = Mathf.Clamp(value, 0.0001f, MaxDelay);

                    _minDelay = value;
                }
            }

            public float MaxDelay
            {
                get => _maxDelay;
                set
                {
                    value = _maxRange.GetClamped(value);
                    
                    value = Mathf.Clamp(value, MinDelay, float.MaxValue);

                    _maxDelay = value;
                }
            }

            public float RandomDelay => Random.Range(_minDelay, _maxDelay);

            public bool SpawnWhenBegin => _spawnWhenBegin;

            public void ChangeAllDelays(float percents, bool positive)
            {
                if (percents <= 0)
                    return;

                var clampedPercents = Mathf.Clamp(percents, 1f, 100f);

                void AddToValue(ref float value)
                {
                    var toAdd = value * (clampedPercents / 100f) * (positive ? 1 : -1);

                    value += toAdd;
                }

                AddToValue(ref _minDelay);

                MinDelay = MinDelay;

                AddToValue(ref _maxDelay);

                MaxDelay = MaxDelay;
            }

            [Serializable]
            private class Range
            {
                [SerializeField] private float _min;

                [SerializeField] private float _max;

                public float GetClamped(float value)
                {
                    var maxClamped = _max;

                    if (_max <= 0)
                        maxClamped = float.MaxValue;
                    
                    return Mathf.Clamp(value, _min, maxClamped);
                }
            }
        }
    }
}