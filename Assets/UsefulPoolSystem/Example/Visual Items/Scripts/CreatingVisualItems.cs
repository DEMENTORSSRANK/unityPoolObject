using UnityEngine;
using UsefulPoolSystem.Core.Scripts;
using Random = UnityEngine.Random;

namespace UsefulPoolSystem.Example.Visual_Items.Scripts
{
    public class CreatingVisualItems : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;

        private Color RandomColor => _colors[Random.Range(0, _colors.Length)];

        private UsefulPoolObjectSystem _poolSystem;
        
        [ContextMenu("Spawn new")]
        private void SpawnNewQueue()
        {
            var newItemSpawned = _poolSystem.Spawn<VisualItemPrefab>();
            
            newItemSpawned.UpdateColor(RandomColor);
        }

        private void Start()
        {
            _poolSystem = UsefulPoolObjectSystem.I;
        }
    }
}