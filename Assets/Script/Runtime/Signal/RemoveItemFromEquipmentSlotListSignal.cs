using Script.Runtime.CollectableModule.ItemModule.Model;
using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct RemoveItemFromEquipmentSlotListSignal
    {
        public readonly ItemSo ItemData;
        
        public RemoveItemFromEquipmentSlotListSignal(ItemSo itemData)
        {
            ItemData = itemData;
        }
    }
}