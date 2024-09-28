using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct InstantiateExpGemSignal
    {
        public readonly Vector3 ExpGemSpawnPosition;
        
        public InstantiateExpGemSignal(Vector3 expGemSpawnPosition)
        {
            ExpGemSpawnPosition = expGemSpawnPosition;
        }
    }
}