using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct ActivateAbilitySignal
    {
        public readonly Transform AbilitySpawnPoint;
        
        public ActivateAbilitySignal(Transform abilitySpawnPoint)
        {
            AbilitySpawnPoint = abilitySpawnPoint;
        }
    }
}