using UnityEngine;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.Spawner_Visual_Items.Scripts
{
    public class VisualColoredCreate : MonoBehaviour
    {
        [SerializeField] private UsefulSpawner<VisualItemColored> spawner;

        [SerializeField] private Color[] colors;

        private Color RandomColor => colors[Random.Range(0, colors.Length)];

        [ContextMenu("Create")]
        private void CreateNew()
        {
            var newItem = spawner.SpawnNewItem();

            newItem.Color = RandomColor;
        }

        [ContextMenu("Clear")]
        private void ClearAll()
        {
            spawner.ClearAll();
        }

        [ContextMenu("Remove random")]
        private void RemoveRandom()
        {
            if (!spawner.TryGetRandomItem(out var item))
                return;
            
            spawner.DeSpawnItem(item);
        }

    }
}