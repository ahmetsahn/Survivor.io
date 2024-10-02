using Script.Ahmet.ObjectPool;
using Script.Runtime.Enum;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Script.Runtime.AbilityModule
{
    public class PowerAreaAbility : Ability
    {
        private readonly PowerAreaConfig[] _powerAreaConfigs;

        private readonly Transform _playerTransform;

        private readonly GameObject _powerAreaPrefab;
        private readonly GameObject _evolvedPowerAreaPrefab;
        private GameObject _activePowerArea;

        public PowerAreaAbility(SignalBus signalBus, PowerAreaAbilityConfig config)
            : base(signalBus)
        {
            AbilityType = AbilityType.PowerArea;
            _powerAreaConfigs = config.PowerAreaLevelConfigs;
            _powerAreaPrefab = config.PowerAreaPrefab;
            _evolvedPowerAreaPrefab = config.EvolvedPowerAreaPrefab;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void ActivateAbility()
        {
            SpawnPowerArea(_powerAreaPrefab);
        }

        protected override void DeactivateAbility()
        {
            ObjectPoolManager.ReturnObjectToPool(_activePowerArea);
        }

        protected override void ActivateEvolvedAbility()
        {
            SpawnPowerArea(_evolvedPowerAreaPrefab);
        }

        protected override void ApplyUpgradeEffect()
        {
            InitializePowerArea(_activePowerArea);
        }

        private void SpawnPowerArea(GameObject prefab)
        {
            _activePowerArea = ObjectPoolManager.SpawnObjectWithZenject(prefab, _playerTransform);
            InitializePowerArea(_activePowerArea);
        }

        private void InitializePowerArea(GameObject powerAreaObject)
        {
            var powerArea = powerAreaObject.GetComponent<PowerArea>();
            var config = _powerAreaConfigs[CurrentLevelIndex];
            powerArea.Initialize(config.Radius, config.Damage);
        }
    }

    [Serializable]
    public struct PowerAreaAbilityConfig
    {
        public GameObject PowerAreaPrefab;
        public GameObject EvolvedPowerAreaPrefab;
        public PowerAreaConfig[] PowerAreaLevelConfigs;
    }

    [Serializable]
    public struct PowerAreaConfig
    {
        public int Damage;
        public float Radius;
    }
}
