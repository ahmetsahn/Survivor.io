using System;
using System.Threading;
using Assets.Script.Runtime.AbilityModule;
using Cysharp.Threading.Tasks;
using Script.Ahmet.ObjectPool;
using Script.Runtime.AbilityModule;
using Script.Runtime.Enum;
using UnityEngine;
using Zenject;

namespace Script.Runtime
{
    public class RpgAbility : Ability
    {
        private readonly float _spawnInterval;
        private readonly float _movementSpeed;
        
        private readonly RpgConfig[] _rpgLevelConfigs;
        
        private readonly GameObject _rpgPrefab;
        private readonly GameObject _evolvedRpgPrefab;
        
        private readonly Transform _playerTransform;
        
        private CancellationTokenSource _rpgSpawnCancellationTokenSource;
        
        public RpgAbility(SignalBus signalBus, RpgAbilityConfig config) : base(signalBus)
        {
            AbilityType = AbilityType.Rpg;
            _spawnInterval = config.SpawnInterval;
            _movementSpeed = config.MovementSpeed;
            _rpgLevelConfigs = config.RpgLevelConfigs;
            _rpgPrefab = config.RpgPrefab;
            _evolvedRpgPrefab = config.EvolvedRpgPrefab;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void ActivateAbility()
        {
            SpawnRpg(_rpgPrefab).Forget();
        }

        protected override void DeactivateAbility()
        {
            CancelRpgSpawnToken();
        }

        protected override void ActivateEvolvedAbility()
        {
            SpawnRpg(_evolvedRpgPrefab).Forget();
        }

        protected override void ApplyUpgradeEffect()
        {
            switch (CurrentLevel)
            {
                case 3:
                    SpawnRpg(_rpgPrefab).Forget();
                    break;
                case 5:
                    SpawnRpg(_rpgPrefab).Forget();
                    break;
            }
        }

        private async UniTask SpawnRpg(GameObject rpgPrefab)
        {
            InitializeRpgSpawnToken();
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            while (!_rpgSpawnCancellationTokenSource.IsCancellationRequested)
            {
                GameObject rpgGameObject = ObjectPoolManager.SpawnObject(rpgPrefab, _playerTransform.position, Quaternion.identity);
                Rpg rpg = rpgGameObject.GetComponent<Rpg>();
                float radius = _rpgLevelConfigs[CurrentLevelIndex].Radius;
                int damage = _rpgLevelConfigs[CurrentLevelIndex].Damage;
                rpg.Initialize(_movementSpeed, radius, damage);
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: _rpgSpawnCancellationTokenSource.Token);
            }
        }

        private void InitializeRpgSpawnToken()
        {
            _rpgSpawnCancellationTokenSource = new CancellationTokenSource();
        }

        private void CancelRpgSpawnToken()
        {
            _rpgSpawnCancellationTokenSource.Cancel();
        }
    }
    
    [Serializable]
    public struct RpgAbilityConfig
    {
        public GameObject RpgPrefab;
        public GameObject EvolvedRpgPrefab;
        
        public float SpawnInterval;
        public float MovementSpeed;
        
        public RpgConfig[] RpgLevelConfigs;
    }
    
    [Serializable]
    public struct RpgConfig
    {
        public int Damage;
        
        public float Radius;
    }
}