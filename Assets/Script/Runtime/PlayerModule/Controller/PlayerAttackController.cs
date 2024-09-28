using System;
using Cysharp.Threading.Tasks;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.PlayerModule.View;
using Script.Runtime.Signal;
using Zenject;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerAttackController : IDisposable
    {
        private readonly PlayerView _view;
        
        private readonly PlayerModel _model;
        
        private readonly SignalBus _signalBus;
        
        public PlayerAttackController(PlayerView view, PlayerModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnStartAttackEvent += OnStartAttack;
            _signalBus.Subscribe<SetPlayerWeaponSignal>(SetPlayerWeapon);
        }
        
        private void OnStartAttack()
        {
            Attack().Forget();
        }
        
        private async UniTaskVoid Attack()
        {
            while (true)
            {
                _model.CurrentPlayerWeaponSo.Attack(_view.AimTransform, _model.Damage);
                await UniTask.Delay(TimeSpan.FromSeconds(_model.AttackRate));
            }
        }
        
        private void SetPlayerWeapon(SetPlayerWeaponSignal signal)
        {
            _model.CurrentPlayerWeaponSo = signal.PlayerWeaponSo;
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnStartAttackEvent -= OnStartAttack;
            _signalBus.Unsubscribe<SetPlayerWeaponSignal>(SetPlayerWeapon);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}