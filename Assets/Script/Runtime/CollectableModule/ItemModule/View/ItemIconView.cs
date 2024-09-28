using System;
using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.InventoryModule;
using Script.Runtime.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.View
{
    public class ItemIconView : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image itemIconImage;
        
        [SerializeField]
        private GameObject itemDetailsPanel;
        
        [SerializeField]
        private TextMeshProUGUI itemDetailsText;
        
        [SerializeField]
        private Button equipButton;
        [SerializeField]
        private Button unequipButton;
        [SerializeField]
        private Button dropButton;
        
        [HideInInspector]
        public ItemSo Data;
        
        public event Action OnPointerClickEvent;
        public event Action OnPointerExitEvent;
        
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
            itemIconImage.sprite = data.Icon;
            itemDetailsText.text = data.DetailsText;
        }

        private void OnEnable()
        {
            equipButton.onClick.AddListener(OnEquipButtonClick);
            unequipButton.onClick.AddListener(OnUnequipButtonClick);
            dropButton.onClick.AddListener(OnDropButtonClick);
        }
        
        private void OnEquipButtonClick()
        {
            _signalBus.Fire(new PlaceItemInEquipmentSlotSignal(Data,transform));
            _signalBus.Fire(new PlayerCustomizationSignal(Data));
        }
        
        private void OnUnequipButtonClick()
        {
            if(_inventoryManager.IsInventoryFull()) return;
            
            _signalBus.Fire(new RemoveItemFromEquipmentSlotListSignal(Data));
            _signalBus.Fire(new PlaceItemInInventorySlotSignal(Data,transform));
            _signalBus.Fire(new AddItemToItemListSignal(Data));
            _signalBus.Fire(new PlayerDefaultCustomizationSignal(Data.ItemType));
        }
        
        private void OnDropButtonClick()
        {
            _signalBus.Fire(new RemoveItemFromItemListSignal(transform.GetSiblingIndex()));
            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            itemDetailsPanel.SetActive(true);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            itemDetailsPanel.SetActive(false);
            OnPointerClickEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemDetailsPanel.SetActive(false);
            OnPointerExitEvent?.Invoke();
        }
        
        private void OnDisable()
        {
            equipButton.onClick.RemoveListener(OnEquipButtonClick);
            unequipButton.onClick.RemoveListener(OnUnequipButtonClick);
            dropButton.onClick.RemoveListener(OnDropButtonClick);
        }
    }
}