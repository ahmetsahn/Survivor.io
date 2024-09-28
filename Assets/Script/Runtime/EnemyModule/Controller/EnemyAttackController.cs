using System;
using Script.Runtime.EnemyModule.Model;
using Script.Runtime.EnemyModule.View;
using Script.Runtime.Interface;
using UnityEngine;

namespace Script.Runtime.EnemyModule.Controller
{
    public class EnemyAttackController : IDisposable
    {
        private readonly EnemyView _view;
        
        private readonly EnemyModel _model;
        
        public EnemyAttackController(EnemyView view, EnemyModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnEnemyCollisionEvent += OnEnemyCollision;
        }
        
        private void OnEnemyCollision(Collider2D other)
        {
            if ((_view.TargetLayer & 1 << other.gameObject.layer) == 0)
            {
                return;
            }
            
            if (other.TryGetComponent(out IHealth health))
            {
                health.OnTakeDamageEvent?.Invoke(_model.Data.Damage);
            }
        }

        private void UnsubscribeEvents()
        {
            _view.OnEnemyCollisionEvent -= OnEnemyCollision;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}