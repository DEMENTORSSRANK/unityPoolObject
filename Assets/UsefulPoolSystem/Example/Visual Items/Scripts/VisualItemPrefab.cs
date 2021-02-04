using UnityEngine;
using UnityEngine.UI;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.Visual_Items.Scripts
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