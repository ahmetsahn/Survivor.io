using System;
using Cysharp.Threading.Tasks;
using Script.Ahmet.ObjectPool;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Script.Runtime.SpawnerModule
{
    public class EnemySpawner : IInitializable
    {
        private readonly GameObject _enemyPrefab;

        private readonly float _spawnRate;

        public EnemySpawner(EnemySpawnerConfig config)
        {
            _enemyPrefab = config.EnemyPrefab;
            _spawnRate = config.SpawnRate;
        }

        public void Initialize()
        {
            SpawnEnemy().Forget();
        }

        private async UniTaskVoid SpawnEnemy()
        {
            while (true)
            {
                ObjectPoolManager.SpawnObjectWithZenject(_enemyPrefab, new Vector3(Random.Range(-15, 15),Random.Range(-15,15),0) , Quaternion.identity);
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnRate));
            }
        }
    }

    [Serializable]
    public struct EnemySpawnerConfig
    {
        public GameObject EnemyPrefab;

        public float SpawnRate;
    }
}