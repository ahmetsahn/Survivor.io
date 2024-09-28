using Script.Runtime.CollectableModule.ItemModule.Model;

namespace Script.Runtime.Signal
{
    public readonly struct RemoveItemFromItemListSignal
    {
        public readonly int Index;
        
        public RemoveItemFromItemListSignal(int index)
        {
            Index = index;
        }
    }
}