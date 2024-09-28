namespace Script.Runtime.Signal
{
    public readonly struct IncreasePlayerExpUISignal
    {
        public readonly int ExpValue;
        
        public IncreasePlayerExpUISignal(int expValue)
        {
            ExpValue = expValue;
        }
    }
}