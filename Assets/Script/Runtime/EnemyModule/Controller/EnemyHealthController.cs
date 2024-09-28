using System;
using Script.Ahmet.ObjectPool;
using Script.Runtime.EnemyModule.Model;
using Script.Runtime.EnemyModule.View;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.EnemyModule.Controller
{
    public class EnemyHealthController : IDisposable
    {
        private readonly EnemyView _view;
        
        private readonly EnemyModel _model;
        
        private readonly SignalBus _signalBus;
        
        public EnemyHealthController(EnemyView view, EnemyModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnTakeDamageEvent += OnTakeDamage;
            _view.OnDisableEvent += Reset;
        }
        
        private void OnTakeDamage(int damage)
        {
            _model.Health -= damage;
            
            if (_model.Health <= 0)
            {
                Died();
            }
        }
        
        private void Died()
        {
            ObjectPoolManager.SpawnObjectWithZenject(_model.Data.EnemyDeathParticle, _view.transform.position, Quaternion.identity);
            Vector3 expGemPosition = new Vector3(_view.transform.position.x, _view.transform.position.y + 1, _view.transform.position.z);
            Vector3 itemPosition = new Vector3(_view.transform.position.x, _view.transform.position.y - 1, _view.transform.position.z);
            _signalBus.Fire(new InstantiateExpGemSignal(expGemPosition));
            _signalBus.Fire(new InstantiateItemSignal(itemPosition));
            ObjectPoolManager.ReturnObjectToPool(_view.gameObject);
        }
        
        private void Reset()
        {
            _model.Health = _model.Data.MaxHealth;
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnTakeDamageEvent -= OnTakeDamage;
            _view.OnDisableEvent -= Reset;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}