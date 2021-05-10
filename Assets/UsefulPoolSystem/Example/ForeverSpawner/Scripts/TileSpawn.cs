using System;
using UnityEngine;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Example.ForeverSpawner.Scripts
{
    public class TileSpawn : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;

        [SerializeField] private UsefulSpawner<Tile> _spawner;

        [SerializeField] private bool _isPositiveEverySpawn;

        [Range(0f, 100f)] [SerializeField] private float _percents = 20f;

        private void OnForeverSpawned(Tile tile)
        {
            tile.transform.position = _startPosition;
            
            tile.UpdateDirection();
            
            _spawner.ForeverSpawnParameters.ChangeAllDelays(_percents, _isPositiveEverySpawn);
        }

        [ContextMenu("Start")]
        private void StartSpawn()
        {
            _spawner.StartForeverSpawning(this);
        }

        [ContextMenu("Stop")]
        private void StopSpawn()
        {
            _spawner.StopForeverSpawning(this);
        }

        private void Start()
        {
            _spawner.OnForeverSpawned += OnForeverSpawned;
        }
    }
}