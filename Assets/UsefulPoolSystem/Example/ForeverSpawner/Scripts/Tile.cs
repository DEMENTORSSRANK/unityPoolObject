using System;
using UnityEngine;
using UsefulPoolSystem.Core.Scripts;
using Random = UnityEngine.Random;

namespace UsefulPoolSystem.Example.ForeverSpawner.Scripts
{
    public class Tile : UsefulObject
    {
        [SerializeField] private float _speed = 3f;

        private static float RandomValue => Random.Range(-1f, 1f);

        private Vector2 Direction { get; set; }

        public void UpdateDirection()
        {
            Direction = new Vector2(RandomValue, RandomValue);
        }

        private void MoveToDirection()
        {
            transform.position += (Vector3) Direction * (Time.deltaTime * _speed);
        }

        private void Update()
        {
            MoveToDirection();
        }
    }
}