using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct InstantiateAbilitySignal
    {
        public readonly Transform PlayerTransform;
        
        public InstantiateAbilitySignal(Transform playerTransform)
        {
            PlayerTransform = playerTransform;
        }
    }
}