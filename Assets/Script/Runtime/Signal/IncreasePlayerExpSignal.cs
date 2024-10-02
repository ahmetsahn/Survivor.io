namespace Script.Runtime.Signal
{
    public readonly struct IncreasePlayerExpSignal
    {
        public readonly int ExpValue;
        
        public IncreasePlayerExpSignal(int expValue)
        {
            ExpValue = expValue;
        }
    }
}