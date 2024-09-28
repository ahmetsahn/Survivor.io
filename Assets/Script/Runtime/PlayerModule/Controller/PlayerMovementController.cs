using System;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.PlayerModule.View;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerMovementController : IDisposable
    {
        private  readonly PlayerView _view;
        
        private readonly PlayerModel _model;
        
        public PlayerMovementController(PlayerView view, PlayerModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnMoveEvent += OnMove;
        }
        
        private void OnMove(Vector2 direction)
        {
            _view.transform.position += new Vector3(direction.x, direction.y, 0) * (_model.MovementSpeed * Time.deltaTime);
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnMoveEvent -= OnMove;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}