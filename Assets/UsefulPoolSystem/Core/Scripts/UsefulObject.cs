using UnityEngine;

namespace UsefulPoolSystem.Core.Scripts
{
    public abstract class UsefulObject : MonoBehaviour
    {
        [ContextMenu("DeSpawn")]
        public void DeSpawn()
        {
            if (!UsefulPoolObjectSystem.I)
                return;
            
            UsefulPoolObjectSystem.I.DeSpawn(this);
        }
        
        public virtual void OnSpawned()
        {
            
        }

        public virtual void OnDeSpawned()
        {
            
        }
    }
}