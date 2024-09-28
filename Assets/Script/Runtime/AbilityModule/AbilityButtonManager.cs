using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Script.Runtime.Enum;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Script.Runtime
{
    public class AbilityButtonManager : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly IInstantiator _instantiator;
        
        [SerializedDictionary("AbilityButton","AbilityLevel")]
        private readonly SerializedDictionary<AbilityType,int> _abilityButtonLevelDictionary;
        
        private readonly AbilityButton[] _abilityButtons; 
        
        private const int MAX_ABILITY_BUTTON_COUNT_PER_ROUND = 3;

        private const int MAX_ABILITY_LEVEL = 6;
        
        private int _maxLevelAbilityCount;
        
        public AbilityButtonManager(SignalBus signalBus, IInstantiator instantiator, AbilityButtonManagerConfig config)
        {
            _signalBus = signalBus;
            _instantiator = instantiator;
            _abilityButtonLevelDictionary = config.AbilityButtonLevelDictionary;
            _abilityButtons = config.AbilityButtons;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<AbilityButtonClickSignal>(OnAbilityButtonClick);
        }
        
        public void InstantiateAbilityButtons(Transform parent)
        {
            HashSet<AbilityButton> usedButtons = new HashSet<AbilityButton>();
            
            for (int i = 0; i < MAX_ABILITY_BUTTON_COUNT_PER_ROUND - _maxLevelAbilityCount; i++)
            {
                AbilityButton randomAbilityButton = GetRandomAbilityButton(usedButtons);

                GameObject abilityButtonGo = _instantiator.InstantiatePrefab(randomAbilityButton, parent);
                AbilityButton abilityButton = abilityButtonGo.GetComponent<AbilityButton>();
                abilityButton.Initialize(_abilityButtonLevelDictionary[abilityButton.AbilityType]);
            }
        }

        private AbilityButton GetRandomAbilityButton(HashSet<AbilityButton> usedButtons)
        {
            AbilityButton randomKey;

            do
            {
                randomKey = _abilityButtons[Random.Range(0, _abilityButtons.Length)];
            }
            
            while (usedButtons.Contains(randomKey) || _abilityButtonLevelDictionary[randomKey.AbilityType] >= MAX_ABILITY_LEVEL);
            
            usedButtons.Add(randomKey);

            return randomKey;
        }
        
        private void OnAbilityButtonClick(AbilityButtonClickSignal signal)
        {
            _abilityButtonLevelDictionary[signal.AbilityType]++;
            
            _maxLevelAbilityCount = _abilityButtonLevelDictionary.Values.Count(level => level == MAX_ABILITY_LEVEL);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<AbilityButtonClickSignal>(OnAbilityButtonClick);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }

    [Serializable]
    public struct AbilityButtonManagerConfig
    {
        [SerializedDictionary("AbilityButton","AbilityLevel")]
        public SerializedDictionary<AbilityType,int> AbilityButtonLevelDictionary;
        
        public AbilityButton[] AbilityButtons;
    }
}