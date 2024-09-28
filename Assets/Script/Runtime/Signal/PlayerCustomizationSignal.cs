using Script.Runtime.CollectableModule.ItemModule.Model;

namespace Script.Runtime.Signal
{
    public readonly struct PlayerCustomizationSignal
    {
        public readonly ItemSo ItemData;
        
        public PlayerCustomizationSignal(ItemSo itemData)
        {
            ItemData = itemData;
        }
    }
}