using System;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.Signal;
using Script.Runtime.UIModule;
using Zenject;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerExpController : IDisposable
    {
        private readonly PlayerModel _model;
        
        private readonly GameUIPanelView _gameUIPanelView;
        
        private  readonly SignalBus _signalBus;
        
        public PlayerExpController(PlayerModel model, SignalBus signalBus, GameUIPanelView gameUIPanelView)
        {
            _model = model;
            _signalBus = signalBus;
            _gameUIPanelView = gameUIPanelView;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<IncreasePlayerExpUISignal>(IncreasePlayerExpUI);
        }
        
        private void IncreasePlayerExpUI(IncreasePlayerExpUISignal signal)
        {
            if (_model.Level == _model.MAX_LEVEL)
            {
                return;
            }
            
            _model.Exp += signal.ExpValue;
            _gameUIPanelView.PlayerExpBar.fillAmount = (float) _model.Exp / _model.LevelExp[_model.LevelIndex];
            
            if(_model.Exp >= _model.LevelExp[_model.LevelIndex])
            {
                _signalBus.Fire(new PlayerLevelUpSignal());
                _model.Level++;
                _model.LevelIndex++;
                _model.Exp = 0;
                _gameUIPanelView.PlayerLevelText.text = "LV " + _model.Level;
                _gameUIPanelView.PlayerExpBar.fillAmount = 0;
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<IncreasePlayerExpUISignal>(IncreasePlayerExpUI);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}