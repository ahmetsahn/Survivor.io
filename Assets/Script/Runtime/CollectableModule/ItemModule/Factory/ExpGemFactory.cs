using System;
using Script.Ahmet.ObjectPool;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Script.Runtime.CollectableModule.ItemModule.Factory
{
    public class ExpGemFactory : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private GameObject _expGemPrefab;
        
        public ExpGemFactory(SignalBus signalBus, ExpGemFactoryConfig config)
        {
            _signalBus = signalBus;
            _expGemPrefab = config.expGemPrefab;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<InstantiateExpGemSignal>(InstantiateExpGem);
        }
        
        private void InstantiateExpGem(InstantiateExpGemSignal signal)
        {
            ObjectPoolManager.SpawnObjectWithZenject(_expGemPrefab, signal.ExpGemSpawnPosition, Quaternion.identity);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<InstantiateExpGemSignal>(InstantiateExpGem);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public struct ExpGemFactoryConfig
    {
        public GameObject expGemPrefab;
    }
}