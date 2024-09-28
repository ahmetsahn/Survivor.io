using Script.Runtime.Enum;

namespace Script.Runtime.Signal
{
    public readonly struct PlayerDefaultCustomizationSignal
    {
        public readonly ItemType ItemType;
        
        public PlayerDefaultCustomizationSignal(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}