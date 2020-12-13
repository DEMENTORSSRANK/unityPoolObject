using UnityEngine;

namespace UnityPoolObject.Core
{
    public abstract class UnityPoolObject : MonoBehaviour, IPoolObject<string>
    {
        public virtual string Group => name; // та самая группа

        public Transform MyTransform { get; private set; }

        protected virtual void Awake()
        {
            MyTransform = transform;
        }

        public virtual void SetTransform(Vector3 position, Quaternion rotation)
        {
            MyTransform.position = position;
            MyTransform.rotation = rotation;
        }

        public virtual void Create()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPush()
        {
            gameObject.SetActive(false);
        }

        public virtual void Push()
        {
            UnityPoolManager.Instance.Push(Group, this);
        }

        public void FailedPush()
        {
            Debug.Log("FailedPush"); // !!!
            
            Destroy(gameObject);
        }
    }
}