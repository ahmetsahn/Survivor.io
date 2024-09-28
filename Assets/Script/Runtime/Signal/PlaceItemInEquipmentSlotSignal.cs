using Script.Runtime.CollectableModule.ItemModule.Model;
using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct PlaceItemInEquipmentSlotSignal
    {
        public readonly ItemSo ItemData;
        
        public readonly Transform ItemIconTransform;
        
        public PlaceItemInEquipmentSlotSignal(ItemSo itemData, Transform ıtemIconTransform)
        {
            ItemData = itemData;
            ItemIconTransform = ıtemIconTransform;
        }
    }
}