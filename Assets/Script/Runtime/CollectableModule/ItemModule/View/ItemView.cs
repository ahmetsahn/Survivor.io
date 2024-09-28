using Script.Ahmet.ObjectPool;
using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.Interface;
using Script.Runtime.InventoryModule;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.View
{
    public class ItemView : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public ItemSo Data;
     
        private SignalBus _signalBus;
        
        private InventoryManager _inventoryManager;
        
        [Inject]
        private void Construct(SignalBus signalBus, InventoryManager inventoryManager)
        {
            _signalBus = signalBus;
            _inventoryManager = inventoryManager;
        }
        
        public void Initialize(ItemSo data)
        {
            Data = data;
            spriteRenderer.sprite = data.Icon;
        }
        public void Collect()
        {
            if(_inventoryManager.IsInventoryFull()) return;
            
            _signalBus.Fire(new AddItemToItemListSignal(Data));
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}