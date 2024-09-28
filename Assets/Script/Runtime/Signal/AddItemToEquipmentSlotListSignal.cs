using Script.Runtime.CollectableModule.ItemModule.Model;
using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct AddItemToEquipmentSlotListSignal
    {
        public readonly ItemSo ItemData;
        
        public AddItemToEquipmentSlotListSignal(ItemSo itemData)
        {
            ItemData = itemData;
        }
    }
}