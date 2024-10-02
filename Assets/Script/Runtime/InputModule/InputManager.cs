using System;
using Script.Runtime.Enum;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.InputModule
{
    public class InputManager : ITickable
    {
        private readonly SignalBus _signalBus;
        
        private readonly KeyCode _hideAllUIPanelsKey;
        private readonly KeyCode _showInventoryKey;
        
        public InputManager(SignalBus signalBus, InputManagerConfig config)
        {
            _signalBus = signalBus;
            _hideAllUIPanelsKey = config.HideAllUIPanelsKey;
            _showInventoryKey = config.ShowInventoryKey;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(_hideAllUIPanelsKey))
            {
                Time.timeScale = 1;
                _signalBus.Fire(new HideAllUIPanelsSignal());
            }
            
            if (Input.GetKeyDown(_showInventoryKey))
            {
                Time.timeScale = 0;
                _signalBus.Fire(new ShowUIPanelSignal(UIPanelType.InventoryPanel));
            }
        }
    }
    
    [Serializable]
    public struct InputManagerConfig
    {
        public KeyCode HideAllUIPanelsKey;
        public KeyCode ShowInventoryKey;
    }
}