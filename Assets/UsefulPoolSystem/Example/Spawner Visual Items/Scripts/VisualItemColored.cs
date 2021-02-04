using System;
using UnityEngine;
using UnityEngine.UI;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.Spawner_Visual_Items.Scripts
{
    [RequireComponent(typeof(Image))]
    public class VisualItemColored : UsefulObject
    {
        private Image _image;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
    }
}