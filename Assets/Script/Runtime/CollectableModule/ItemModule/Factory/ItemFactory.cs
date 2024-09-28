using System;
using Script.Ahmet.ObjectPool;
using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.CollectableModule.ItemModule.View;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Script.Runtime.CollectableModule.ItemModule.Factory
{
    public class ItemFactory : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly GameObject _dropItemPrefab;
        
        private readonly ItemSo[] _dropItemsData;
        
        private readonly int[] _dropItemWeights;
        
        public ItemFactory(SignalBus signalBus, ItemFactoryConfig config)
        {
            _signalBus = signalBus;
            _dropItemPrefab = config.dropItemPrefab;
            _dropItemsData = config.dropItemsData;
            _dropItemWeights = config.dropItemWeights;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<InstantiateItemSignal>(DropItem);
        }
        
        private void DropItem(InstantiateItemSignal signal)
        {
            var randomValue = GetRandomIndex();
            if (randomValue == _dropItemsData.Length)
            {
                return;
            }
            ItemSo dropItemData = _dropItemsData[randomValue];
            GameObject dropItem = ObjectPoolManager.SpawnObjectWithZenject(_dropItemPrefab, signal.TransformPosition, Quaternion.identity);
            dropItem.GetComponent<ItemView>().Initialize(dropItemData);
        }
        
        private int GetRandomIndex()
        {
            int totalWeight = 0;
            foreach (int weight in _dropItemWeights)
            {
                totalWeight += weight;
            }
            
            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;
            for (int i = 0; i < _dropItemWeights.Length; i++)
            {
                cumulativeWeight += _dropItemWeights[i];
                if (randomValue < cumulativeWeight)
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<InstantiateItemSignal>(DropItem);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public struct ItemFactoryConfig
    {
        public GameObject dropItemPrefab;
        public ItemSo[] dropItemsData;
        public int[] dropItemWeights;
    }
}