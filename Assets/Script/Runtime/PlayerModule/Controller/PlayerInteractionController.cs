using System;
using Script.Runtime.Interface;
using Script.Runtime.PlayerModule.View;
using UnityEngine;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerInteractionController : IDisposable
    {
        private readonly PlayerView _view;
        
        public PlayerInteractionController(PlayerView view)
        {
            _view = view;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnPlayerCollisionEvent += OnPlayerCollision;
        }
        
        private void OnPlayerCollision(Collider2D other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnPlayerCollisionEvent -= OnPlayerCollision;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}