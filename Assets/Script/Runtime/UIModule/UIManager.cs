using System;
using Script.Runtime.Enum;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.UIModule
{
    public class UIManager : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public UIManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PlayerLevelUpSignal>(PlayerLevelUp);
        }
        
        private void PlayerLevelUp(PlayerLevelUpSignal signal)
        {
            _signalBus.Fire(new ShowUIPanelSignal(UIPanelType.AbilitySelectionPanel));
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PlayerLevelUpSignal>(PlayerLevelUp);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}