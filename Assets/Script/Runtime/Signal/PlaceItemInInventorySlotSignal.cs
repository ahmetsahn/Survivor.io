using Script.Runtime.CollectableModule.ItemModule.Model;
using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct PlaceItemInInventorySlotSignal
    {
        public readonly ItemSo ItemData;
        
        public readonly Transform ItemIconTransform;
        
        public PlaceItemInInventorySlotSignal(ItemSo itemData, Transform ıtemIconTransform)
        {
            ItemData = itemData;
            ItemIconTransform = ıtemIconTransform;
        }
    }
}