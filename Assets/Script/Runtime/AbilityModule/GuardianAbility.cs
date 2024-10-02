using Script.Ahmet.ObjectPool;
using Script.Runtime.Enum;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Script.Runtime.AbilityModule
{
    public class GuardianAbility : Ability
    {
        private readonly GuardianConfig[] _guardianLevelConfigs;

        private readonly Transform _playerTransform;

        private GameObject _guardianGameObject;
        private GameObject _evolvedGuardianGameObject;
        private readonly GameObject _guardianPrefab;
        private readonly GameObject _evolvedGuardianPrefab;

        public GuardianAbility(SignalBus signalBus, GuardianAbilityConfig config)
            : base(signalBus)
        {
            AbilityType = AbilityType.Guardian;
            _guardianLevelConfigs = config.GuardianLevelConfigs;
            _guardianPrefab = config.GuardianPrefab;
            _evolvedGuardianPrefab = config.EvolvedGuardianPrefab;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void ActivateAbility()
        {
            SpawnGuardianPrefab();
            InitializeGuardian(_guardianGameObject);
        }

        protected override void DeactivateAbility()
        {
            GameObject.Destroy(_guardianGameObject);
        }

        protected override void ActivateEvolvedAbility()
        {
            SpawnEvolvedGuardianPrefab();
            InitializeGuardian(_evolvedGuardianGameObject);
        }

        protected override void ApplyUpgradeEffect()
        {
            InitializeGuardian(_guardianGameObject);
        }

        private void SpawnGuardianPrefab()
        {
            _guardianGameObject = ObjectPoolManager.SpawnObjectWithZenject(_guardianPrefab, _playerTransform);
        }

        private void SpawnEvolvedGuardianPrefab()
        {
            _evolvedGuardianGameObject = ObjectPoolManager.SpawnObjectWithZenject(_evolvedGuardianPrefab, _playerTransform);
        }

        private void InitializeGuardian(GameObject guardianGameObject)
        {
            var guardian = guardianGameObject.GetComponent<Guardian>();
            float spinSpeed = _guardianLevelConfigs[CurrentLevelIndex].SpinSpeed;
            guardian.SetSpeed(spinSpeed);

            if (CurrentLevelIndex < MAX_ABILITY_LEVEL - 1)
            {
                guardian.ActivateParticle(CurrentLevel);
            }

            GuardianCollider[] childColliders = guardianGameObject.GetComponentsInChildren<GuardianCollider>();
            int damage = _guardianLevelConfigs[CurrentLevelIndex].Damage;

            foreach (var collider in childColliders)
            {
                collider.SetDamage(damage);
            }
        }
    }

    [Serializable]
    public struct GuardianAbilityConfig
    {
        public GameObject GuardianPrefab;
        public GameObject EvolvedGuardianPrefab;
        public GuardianConfig[] GuardianLevelConfigs;
    }

    [Serializable]
    public struct GuardianConfig
    {
        public int Damage;
        public float SpinSpeed;
    }
}
