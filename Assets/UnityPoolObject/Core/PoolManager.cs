using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPoolObject.Core
{
    public sealed class PoolManager<TK, TV> where TV : IPoolObject<TK>
    {
        public int MaxInstances { get; }

        public int InstanceCount => _objects.Count;

        public int CacheCount => _cache.Count;

        public delegate bool Compare<in T>(T value) where T : TV;

        private readonly Dictionary<TK, List<TV>> _objects;

        private readonly Dictionary<Type, List<TV>> _cache;

        public PoolManager(int maxInstance)
        {
            MaxInstances = maxInstance;
            _objects = new Dictionary<TK, List<TV>>();
            _cache = new Dictionary<Type, List<TV>>();
        }

        /// <summary>
        /// Check if we can push new object
        /// </summary>
        /// <returns></returns>
        public bool CanPush()
        {
            return InstanceCount + 1 < MaxInstances;
        }

        /// <summary>
        /// Push object by group key
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Push(TK groupKey, TV value)
        {
            const bool result = false;

            if (CanPush())
            {
                value.OnPush();

                if (!_objects.ContainsKey(groupKey))
                {
                    _objects.Add(groupKey, new List<TV>());
                }

                _objects[groupKey].Add(value);

                var type = value.GetType();

                if (!_cache.ContainsKey(type))
                {
                    _cache.Add(type, new List<TV>());
                }

                _cache[type].Add(value);
            }
            else
            {
                value.FailedPush();
            }

            return result;
        }

        public T Pop<T>(TK groupKey) where T : TV
        {
            var result = default(T);

            if (!Contains(groupKey) || _objects[groupKey].Count <= 0)
                return result;

            for (var i = 0; i < _objects[groupKey].Count; i++)
            {
                if (!(_objects[groupKey][i] is T))
                    continue;

                result = (T) _objects[groupKey][i];

                var type = result.GetType();

                RemoveObject(groupKey, i);

                RemoveFromCache(result, type);

                result.Create();

                break;
            }

            return result;
        }

        public T Pop<T>() where T : TV
        {
            var result = default(T);
            var type = typeof(T);
            if (!ValidateForPop(type))
                return result;

            for (var i = 0; i < _cache[type].Count; i++)
            {
                result = (T) _cache[type][i];

                if (result == null || !_objects.ContainsKey(result.Group))
                    continue;

                _objects[result.Group].Remove(result);

                RemoveFromCache(result, type);

                result.Create();

                break;
            }

            return result;
        }

        public T Pop<T>(Compare<T> comparer) where T : TV
        {
            var result = default(T);

            var type = typeof(T);

            if (!ValidateForPop(type))
                return result;

            for (var i = 0; i < _cache[type].Count; i++)
            {
                var value = (T) _cache[type][i];
                if (!comparer(value))
                    continue;

                _objects[value.Group].Remove(value);

                RemoveFromCache(result, type);

                result = value;

                result.Create();

                break;
            }

            return result;
        }


        public bool Contains(TK groupKey)
        {
            return _objects.ContainsKey(groupKey);
        }

        public void Clear()
        {
            _objects.Clear();
        }

        private bool ValidateForPop(Type type)
        {
            return _cache.ContainsKey(type) && _cache[type].Count > 0;
        }

        private void RemoveObject(TK groupKey, int idx)
        {
            if (idx < 0 || idx >= _objects[groupKey].Count)
                return;

            _objects[groupKey].RemoveAt(idx);

            if (_objects[groupKey].Count == 0)
            {
                _objects.Remove(groupKey);
            }
        }

        private void RemoveFromCache(TV value, Type type)
        {
            if (!_cache.ContainsKey(type))
                return;

            _cache[type].Remove(value);

            if (_cache[type].Count == 0)
            {
                _cache.Remove(type);
            }
        }
    }
}