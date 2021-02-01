using UnityEngine;
using UnityEngine.UI;
using UsefulPoolObject.Core.Scripts;

namespace UsefulPoolObject.Example.Visual_Items.Scripts
{
    public class VisualItemPrefab : UsefulObject
    {
        [SerializeField] private Image image;

        public void UpdateColor(Color color)
        {
            image.color = color;
        }
    }
}