using System;
using UnityEngine;

namespace UnityPoolObject.Core
{
    public class UnityPoolManager : MonoBehaviour
    {
        [SerializeField] private int maxInstanceCount = 128;
        
        public static UnityPoolManager Instance { get; private set; }
        
        protected PoolManager<string, UnityPoolObject> poolManager; 


    protected virtual void Awake()
    {
        Instance = this;
        poolManager = new PoolManager<string, UnityPoolObject>(maxInstanceCount);
    }


    public virtual bool CanPush()
    {
        return poolManager.CanPush();
    }

    public virtual bool Push(string groupKey, UnityPoolObject poolObject)
    {
        return poolManager.Push(groupKey, poolObject);
    }

    public virtual T PopOrCreate<T>(T prefab) where T : UnityPoolObject
    {
        return PopOrCreate(prefab, Vector3.zero, Quaternion.identity);
    }

    public virtual T PopOrCreate<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {
        var result = poolManager.Pop<T>(prefab.Group);
        
        if (result == null)
        {
            result = CreateObject<T>(prefab, position, rotation);
        }
        else
        {
            result.SetTransform(position, rotation);
        }
        return result;
    }

    public virtual UnityPoolObject Pop(string groupKey)
    {
        return poolManager.Pop<UnityPoolObject>(groupKey);
    }

    public virtual T Pop<T>() where T : UnityPoolObject
    {
        return poolManager.Pop<T>();
    }

    public virtual T Pop<T>(PoolManager<string, UnityPoolObject>.Compare<T> comparer) where T : UnityPoolObject
    {
        return poolManager.Pop<T>(comparer);
    }

    public virtual T Pop<T>(string groupKey) where T : UnityPoolObject
    {
        return poolManager.Pop<T>(groupKey);
    }

    public virtual bool Contains(string groupKey)
    {
        return poolManager.Contains(groupKey);
    }

    public virtual void Clear()
    {
        poolManager.Clear();
    }

    protected virtual T CreateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {
        GameObject go = Instantiate(prefab.gameObject, position, rotation) as GameObject;
        T result = go.GetComponent<T>();
        result.name = prefab.name;
        return result;
    }
    }
}