using Script.Runtime.Enum;

namespace Script.Runtime.Signal
{
    public readonly struct AbilityButtonClickSignal
    {
        public readonly AbilityType AbilityType;
        
        public readonly int AbilityLevel;
        
        public AbilityButtonClickSignal(AbilityType abilityType, int abilityLevel)
        {
            AbilityType = abilityType;
            AbilityLevel = abilityLevel;
        }
    }
}