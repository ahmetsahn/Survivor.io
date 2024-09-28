using Script.Runtime.CollectableModule.ItemModule.Model;

namespace Script.Runtime.Signal
{
    public readonly struct AddItemToItemListSignal
    {
        public readonly ItemSo ItemData;
        
        public AddItemToItemListSignal(ItemSo itemData)
        {
            ItemData = itemData;
        }
    }
}