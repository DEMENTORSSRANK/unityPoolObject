using UnityEngine;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.Spawner_Visual_Items.Scripts
{
    public class VisualColoredCreate : MonoBehaviour
    {
        [SerializeField] private UsefulSpawner<VisualItemColored> _spawner;

        [SerializeField] private Color[] _colors;

        private Color RandomColor => _colors[Random.Range(0, _colors.Length)];

        [ContextMenu("Create")]
        private void CreateNew()
        {
            var newItem = _spawner.SpawnNewItem();

            newItem.Color = RandomColor;
        }

        [ContextMenu("Clear")]
        private void ClearAll()
        {
            _spawner.ClearAll();
        }

        [ContextMenu("Remove random")]
        private void RemoveRandom()
        {
            if (!_spawner.TryGetRandomItem(out var item))
                return;
            
            _spawner.DeSpawnItem(item);
        }

    }
}