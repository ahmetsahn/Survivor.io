using Script.Runtime.Enum;
using System;
using Zenject;
using Script.Runtime.Signal;

namespace Assets.Script.Runtime.AbilityModule
{
    public abstract class Ability : IDisposable
    {
        protected AbilityType AbilityType;
        protected int CurrentLevel{ get; private set; }
        protected int CurrentLevelIndex { get; private set; }
       
        protected const int MAX_ABILITY_LEVEL = 6;

        private readonly SignalBus _signalBus;
        
        protected abstract void ActivateAbility();
        protected abstract void DeactivateAbility();
        protected abstract void ActivateEvolvedAbility();
        protected abstract void ApplyUpgradeEffect();

        protected Ability(SignalBus signalBus)
        {
            _signalBus = signalBus;
            CurrentLevel = 0;
            CurrentLevelIndex = -1;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _signalBus.Subscribe<AbilityButtonClickSignal>(OnAbilityButtonClick);
        }

        private void OnAbilityButtonClick(AbilityButtonClickSignal signal)
        {
            if (signal.AbilityType != AbilityType) return;

            IncrementAbilityLevel();
            IncrementLevelIndex();

            if (signal.AbilityLevel == 0)
            {
                ActivateAbility();
                return;
            }

            UpgradeAbility();
        }

        private void IncrementAbilityLevel() => CurrentLevel++;
        private void IncrementLevelIndex() => CurrentLevelIndex++;

        private void UpgradeAbility()
        {
            if (CurrentLevel >= MAX_ABILITY_LEVEL)
            {
                DeactivateAbility();
                ActivateEvolvedAbility();
                return;
            }

            ApplyUpgradeEffect();
        }

        private void UnsubscribeFromEvents()
        {
            _signalBus.Unsubscribe<AbilityButtonClickSignal>(OnAbilityButtonClick);
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}
