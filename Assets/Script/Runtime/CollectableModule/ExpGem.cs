using Script.Ahmet.ObjectPool;
using Script.Runtime.Interface;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.CollectableModule
{
    public class ExpGem : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private ExpGemSo data;
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Collect()
        {
            _signalBus.Fire(new IncreasePlayerExpUISignal(data.ExpValue));
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}