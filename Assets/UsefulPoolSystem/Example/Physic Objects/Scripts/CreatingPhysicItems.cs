using UnityEngine;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.Physic_Objects.Scripts
{
    public class CreatingPhysicItems : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;

        [ContextMenu("Spawn")]
        private void SpawnNew()
        {
            var newItem = UsefulPoolObjectSystem.I.Spawn<PhysicPrefab>();

            newItem.transform.position = _startPosition;
        }
    }
}