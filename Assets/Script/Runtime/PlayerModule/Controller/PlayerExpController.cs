using System;
using Assets.Script.Runtime.Signal;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.Signal;
using Zenject;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerExpController : IDisposable
    {
        private readonly PlayerModel _model;
        
        private  readonly SignalBus _signalBus;
        
        public PlayerExpController(PlayerModel model, SignalBus signalBus)
        {
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<IncreasePlayerExpSignal>(IncreasePlayerExpUI);
        }
        
        private void IncreasePlayerExpUI(IncreasePlayerExpSignal signal)
        {
            if (_model.Level == _model.MAX_LEVEL)
            {
                return;
            }
            
            _model.Exp += signal.ExpValue;
            _signalBus.Fire(new UpdatePlayerExpUISignal(_model.Exp, _model.LevelExp[_model.LevelIndex]));
            
            if(_model.Exp >= _model.LevelExp[_model.LevelIndex])
            {
                _model.Level++;
                _model.LevelIndex++;
                _model.Exp = 0;
                _signalBus.Fire(new PlayerLevelUpSignal(_model.Level));
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<IncreasePlayerExpSignal>(IncreasePlayerExpUI);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}