using Script.Runtime.EnemyModule.Model;
using Script.Runtime.EnemyModule.View;
using Script.Runtime.PlayerModule.View;
using UnityEngine;
using Zenject;

namespace Script.Runtime.EnemyModule.Controller
{
    public class EnemyMovementController : ITickable
    {
        private readonly EnemyView _view;
        
        private readonly EnemyModel _model;
        
        private Transform _playerTransform;
        
        private Vector3 _direction;
        
        public EnemyMovementController(EnemyView view, EnemyModel model)
        {
            _view = view;
            _model = model;
            
            FindPlayerTransform();
        }
        
        private void FindPlayerTransform()
        {
            _playerTransform = Object.FindObjectOfType<PlayerView>().transform;
        }
        
        public void Tick()
        {
            MoveToPlayer();
            RotateToPlayer();
        }
        
        private void MoveToPlayer()
        {
            _direction = _playerTransform.position - _view.transform.position;
            _view.transform.position += _direction.normalized * (_model.Data.MovementSpeed * Time.deltaTime);
        }
        
        private void RotateToPlayer()
        {
            _view.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _view.transform.localScale = _direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}