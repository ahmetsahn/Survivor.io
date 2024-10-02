using System;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.PlayerModule.View;
using UnityEngine;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerHealthController : IDisposable
    {
        private readonly PlayerView _view;
        
        private readonly PlayerModel _model;
        
        public PlayerHealthController(PlayerView view, PlayerModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnTakeDamageEvent += OnTakeDamage;
        }
        
        private void OnTakeDamage(int damage)
        {
            _model.Health -= damage;
            _view.HealthBar.fillAmount = (float) _model.Health / _model.MaxHealth;
            
            if (_model.Health <= 0)
            {
                // Player is dead
            }
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnTakeDamageEvent -= OnTakeDamage;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}