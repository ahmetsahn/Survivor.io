using System;
using Script.Ahmet.ObjectPool;
using UnityEngine;

namespace Script.Runtime
{
    public class ParticleReturnToPool : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}