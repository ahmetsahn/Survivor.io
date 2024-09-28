using Script.Runtime.CollectableModule.ItemModule.Model;
using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct InstantiateItemIconSignal
    {
        public readonly ItemSo ItemData;
        
        public readonly Transform ItemParentTransform;
        
        public InstantiateItemIconSignal(ItemSo itemData, Transform itemParentTransform)
        {
            ItemData = itemData;
            ItemParentTransform = itemParentTransform;
        }
    }
}