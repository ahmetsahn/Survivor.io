using System;
using Script.Runtime.CollectableModule.ItemModule.View;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.Factory
{
    public class ItemIconFactory : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly IInstantiator _instantiator;
        
        private readonly GameObject _itemPrefab;
        
        public ItemIconFactory(SignalBus signalBus, IInstantiator instantiator, ItemIconFactoryConfig config)
        {
            _signalBus = signalBus;
            _instantiator = instantiator;
            _itemPrefab = config.itemPrefab;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<InstantiateItemIconSignal>(InstantiateItemIcon);
        }
        
        private void InstantiateItemIcon(InstantiateItemIconSignal signal)
        {
            GameObject itemIcon = _instantiator.InstantiatePrefab(_itemPrefab, signal.ItemParentTransform);
            itemIcon.GetComponent<ItemIconView>().Initialize(signal.ItemData);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<InstantiateItemIconSignal>(InstantiateItemIcon);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public struct ItemIconFactoryConfig
    {
        public GameObject itemPrefab;
    }
}