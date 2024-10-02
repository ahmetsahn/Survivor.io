using System;
using System.Collections.Generic;
using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.Enum;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.InventoryModule
{
    public class InventoryManager : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly List<ItemSo> _itemList = new();
        private readonly List<ItemSo> _equipmentSlotList = new();

        private readonly Dictionary<ItemType, Sprite[]> equippedSprites = new();

        private const int INVENTORY_CAPACITY = 16;
        
        public InventoryManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<AddItemToItemListSignal>(AddItemToItemList);
            _signalBus.Subscribe<RemoveItemFromItemListSignal>(RemoveItemFromItemList);
            _signalBus.Subscribe<AddItemToEquipmentSlotListSignal>(AddItemToEquipmentSlotDictionary);
            _signalBus.Subscribe<RemoveItemFromEquipmentSlotListSignal>(RemoveItemFromEquipmentSlotDictionary);
        }
        
        private void AddItemToItemList(AddItemToItemListSignal signal)
        {
            AddItemToItemList(signal.ItemData);
        }

        private void AddItemToItemList(ItemSo itemData)
        {
            _itemList.Add(itemData);
        }
        
        private void AddItemToEquipmentSlotDictionary(AddItemToEquipmentSlotListSignal signal)
        {
            AddItemToEquipmentSlotDictionary(signal.ItemData);
        }
        
        private void AddItemToEquipmentSlotDictionary(ItemSo itemData)
        {
            _equipmentSlotList.Add(itemData);
        }
        
        public void UpdateItemIconsOnInventory(Transform itemParentTransform)
        {
            if(_itemList.Count == 0) return;
            
            foreach (var itemData in _itemList)
            {
                _signalBus.Fire(new InstantiateItemIconSignal(itemData,itemParentTransform));
            }
        }
        
        public void UpdateItemIconsOnEquipmentSlots(Dictionary<ItemType,RectTransform> equipmentSlotDictionary)
        {
            if(_equipmentSlotList.Count == 0) return;
            
            foreach (var itemData in _equipmentSlotList)
            {
                _signalBus.Fire(new InstantiateItemIconSignal(itemData, equipmentSlotDictionary[itemData.ItemType]));
            }
        }
        
        public void OnPlaceItemInEquipmentSlot(Dictionary<ItemType, RectTransform> equipmentSlotDictionary, ItemSo itemData, Transform itemIconTransform)
        {
            if (equipmentSlotDictionary.TryGetValue(itemData.ItemType, out var equipmentSlot))
            {
                if (equipmentSlot.childCount > 0)
                {
                    int itemIndex = itemIconTransform.GetSiblingIndex();
                    Transform equippedItem = equipmentSlot.GetChild(0);
                    equippedItem.SetParent(itemIconTransform.parent);
                    equippedItem.SetSiblingIndex(itemIndex);
                    _itemList[itemIndex] = _equipmentSlotList[0];
                    _equipmentSlotList[0] = itemData;
                    itemIconTransform.SetParent(equipmentSlot);
                    itemIconTransform.localPosition = Vector3.zero;
                    return;
                }
                
                RemoveItemFromItemList(itemIconTransform.GetSiblingIndex());
                itemIconTransform.SetParent(equipmentSlot);
                itemIconTransform.localPosition = Vector3.zero;
                AddItemToEquipmentSlotDictionary(itemData);
            }
        }
        
        public void OnPlaceItemInInventorySlot(Transform itemIconTransform, Transform itemParentTransform)
        {
            itemIconTransform.SetParent(itemParentTransform);
            itemIconTransform.localPosition = Vector3.zero;
        }

        public void UpdateEquippedSprites(ItemType itemType, Sprite[] sprites)
        {
            if (equippedSprites.ContainsKey(itemType))
            {
                equippedSprites[itemType] = sprites;
            }
            else
            {
                equippedSprites.Add(itemType, sprites);
            }
        }
        public Sprite[] GetEquippedSprites(ItemType itemType)
        {
            return equippedSprites.ContainsKey(itemType) ? equippedSprites[itemType] : null;
        }

        private void RemoveItemFromItemList(RemoveItemFromItemListSignal signal)
        {
            RemoveItemFromItemList(signal.Index);
        }

        private void RemoveItemFromItemList(int itemIndex)
        {
            _itemList.RemoveAt(itemIndex);
        }
        
        private void RemoveItemFromEquipmentSlotDictionary(RemoveItemFromEquipmentSlotListSignal signal)
        { 
            RemoveItemFromEquipmentSlotDictionary(signal.ItemData);
        }
        
        private void RemoveItemFromEquipmentSlotDictionary(ItemSo itemData)
        { 
            _equipmentSlotList.Remove(itemData);
        }
        
        public bool IsInventoryFull()
        {
            return _itemList.Count == INVENTORY_CAPACITY;
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<AddItemToItemListSignal>(AddItemToItemList);
            _signalBus.Unsubscribe<RemoveItemFromItemListSignal>(RemoveItemFromItemList);
            _signalBus.Unsubscribe<AddItemToEquipmentSlotListSignal>(AddItemToEquipmentSlotDictionary);
            _signalBus.Unsubscribe<RemoveItemFromEquipmentSlotListSignal>(RemoveItemFromEquipmentSlotDictionary);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}