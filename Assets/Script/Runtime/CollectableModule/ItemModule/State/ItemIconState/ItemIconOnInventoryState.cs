using System;
using UnityEngine;


namespace Script.Runtime.CollectableModule.ItemModule.State.ItemIconState
{
    public class ItemIconOnInventoryState : ItemIconBaseState
    {
        public ItemIconOnInventoryState(ItemIconOnInventoryStateConfig config) : base(config.interactPanel) { }
    }
    
    [Serializable]
    public struct ItemIconOnInventoryStateConfig
    {
        public GameObject interactPanel;
    }
}