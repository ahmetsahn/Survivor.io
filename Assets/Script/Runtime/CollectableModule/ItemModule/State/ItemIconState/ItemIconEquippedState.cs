using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.CollectableModule.ItemModule.State.ItemIconState
{
    public class ItemIconEquippedState : ItemIconBaseState
    {
        public ItemIconEquippedState(ItemIconEquippedStateConfig config) : base(config.interactPanel) { }
    }
    
    [Serializable]
    public struct ItemIconEquippedStateConfig
    {
        public GameObject interactPanel;
    }
}