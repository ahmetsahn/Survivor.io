using UnityEngine;

namespace Script.Runtime.Signal
{
    public readonly struct InstantiateItemSignal
    {
        public readonly Vector3 TransformPosition;
        
        public InstantiateItemSignal(Vector3 transformPosition)
        {
            TransformPosition = transformPosition;
        }
    }
}