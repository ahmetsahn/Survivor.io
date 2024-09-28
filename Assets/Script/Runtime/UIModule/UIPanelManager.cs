using System;
using Script.Ahmet.ObjectPool;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Script.Runtime.UIModule
{
    public class UIPanelManager : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly GameObject[] _uIPanelPrefabs;
        
        private readonly Transform[] _layerTransforms;
        
        public UIPanelManager(SignalBus signalBus, UIPanelManagerConfig config)
        {
            _signalBus = signalBus;
            _uIPanelPrefabs = config.uIPanelPrefabs;
            _layerTransforms = config.layerTransforms;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<ShowUIPanelSignal>(ShowPanel);
            _signalBus.Subscribe<HideUIPanelSignal>(HidePanel);
            _signalBus.Subscribe<HideAllUIPanelsSignal>(HideAllPanels);
        }
        
        private void ShowPanel(ShowUIPanelSignal signal)
        {
            int panelIndex = (int)signal.PanelType;
            ObjectPoolManager.SpawnObjectWithZenject(_uIPanelPrefabs[panelIndex], _layerTransforms[panelIndex]);
        }
        
        private void HidePanel(HideUIPanelSignal signal)
        {
            int panelIndex = (int)signal.PanelType;
            var child = _layerTransforms[panelIndex].GetChild(0);
            if (child != null)
            {
                Object.Destroy(child.gameObject);
            }
        }
        
        private void HideAllPanels()
        {
            foreach (var layer in _layerTransforms)
            {
                foreach (Transform child in layer)
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<ShowUIPanelSignal>(ShowPanel);
            _signalBus.Unsubscribe<HideUIPanelSignal>(HidePanel);
            _signalBus.Unsubscribe<HideAllUIPanelsSignal>(HideAllPanels);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public struct UIPanelManagerConfig
    {
        public GameObject[] uIPanelPrefabs;
        
        public Transform[] layerTransforms;
    }
}