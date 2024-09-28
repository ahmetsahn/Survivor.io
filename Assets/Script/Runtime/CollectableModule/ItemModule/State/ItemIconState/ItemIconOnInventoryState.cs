using System;
using Script.Runtime.CollectableModule.ItemModule.View;
using Script.Runtime.Signal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = System.Object;

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