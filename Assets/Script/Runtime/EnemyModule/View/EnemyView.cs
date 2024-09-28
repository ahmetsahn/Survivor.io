using System;
using Script.Runtime.Interface;
using UnityEngine;

namespace Script.Runtime.EnemyModule.View
{
    public class EnemyView : MonoBehaviour, IHealth
    {
        public LayerMask TargetLayer;
        
        public event Action OnDisableEvent;
        
        public event Action<Collider2D> OnEnemyCollisionEvent;
        
        public Action<int> OnTakeDamageEvent { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnemyCollisionEvent?.Invoke(other);
        }

        private void OnDisable()
        {
            OnDisableEvent?.Invoke();
        }
    }
}