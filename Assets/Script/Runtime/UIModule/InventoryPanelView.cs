using System;
using AYellowpaper.SerializedCollections;
using Script.Runtime.Enum;
using Script.Runtime.InventoryModule;
using Script.Runtime.Signal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Runtime.UIModule
{
    public class InventoryPanelView : MonoBehaviour
    {
        [SerializeField] 
        private Transform itemParentTransform;
        
        [SerializeField]
        [SerializedDictionary("ItemType", "RectTransform")]
        private SerializedDictionary<ItemType, RectTransform> equipmentSlotDictionary;

        private SignalBus _signalBus;
        
        private InventoryManager _inventoryManager;
        
        private GameAssets _gameAssets;
        
        [SerializeField]
        private Image[] hoodImages;
        [SerializeField] 
        private Image[] armorImages;
        [SerializeField]
        private Image[] weaponImages;
        [SerializeField]
        private Image[] shoesImages;
        
        [Inject]
        public void Construct(SignalBus signalBus, InventoryManager inventoryManager, GameAssets gameAssets)
        {
            _signalBus = signalBus;
            _inventoryManager = inventoryManager;
            _gameAssets = gameAssets;
        }
        
        private void OnEnable()
        {
            _inventoryManager.UpdateItemIconsOnInventory(itemParentTransform);
            _inventoryManager.UpdateItemIconsOnEquipmentSlots(equipmentSlotDictionary);
            LoadSavedCustomizationSprites();
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PlaceItemInEquipmentSlotSignal>(OnPlaceItemInEquipmentSlot);
            _signalBus.Subscribe<PlaceItemInInventorySlotSignal>(OnPlaceItemInInventorySlot);
            _signalBus.Subscribe<PlayerCustomizationSignal>(OnPlayerCustomization);
            _signalBus.Subscribe<PlayerDefaultCustomizationSignal>(OnPlayerDefaultCustomization);
        }
        
        private void OnPlaceItemInEquipmentSlot(PlaceItemInEquipmentSlotSignal signal)
        {
            _inventoryManager.OnPlaceItemInEquipmentSlot(equipmentSlotDictionary, signal.ItemData, signal.ItemIconTransform);
        }
        
        private void OnPlaceItemInInventorySlot(PlaceItemInInventorySlotSignal signal)
        {
            _inventoryManager.OnPlaceItemInInventorySlot(signal.ItemIconTransform, itemParentTransform);
        }
        
        private void OnPlayerCustomization(PlayerCustomizationSignal signal)
        {
            Image[] targetImages = null;

            switch (signal.ItemData.ItemType)
            {
                case ItemType.Hood:
                    targetImages = hoodImages;
                    break;
                case ItemType.Armor:
                    targetImages = armorImages;
                    break;
                case ItemType.Weapon:
                    targetImages = weaponImages;
                    break;
                case ItemType.Shoes:
                    targetImages = shoesImages;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            signal.ItemData.CustomizationUI(targetImages);

            _inventoryManager.UpdateEquippedSprites(signal.ItemData.ItemType, GetCurrentSprites(targetImages));
        }
        
        private void OnPlayerDefaultCustomization(PlayerDefaultCustomizationSignal signal)
        {
            switch (signal.ItemType)
            {
                case ItemType.Hood:
                    SetSprite(hoodImages, _gameAssets.DefaultHoodSprites);
                    _inventoryManager.UpdateEquippedSprites(ItemType.Hood, _gameAssets.DefaultHoodSprites);
                    break;
                case ItemType.Armor:
                    SetSprite(armorImages, _gameAssets.DefaultArmorSprites);
                    _inventoryManager.UpdateEquippedSprites(ItemType.Armor, _gameAssets.DefaultArmorSprites);
                    break;
                case ItemType.Weapon:
                    SetSprite(weaponImages, _gameAssets.DefaultWeaponSprites);
                    _inventoryManager.UpdateEquippedSprites(ItemType.Weapon, _gameAssets.DefaultWeaponSprites);
                    break;
                case ItemType.Shoes:
                    SetSprite(shoesImages, _gameAssets.DefaultShoesSprites);
                    _inventoryManager.UpdateEquippedSprites(ItemType.Shoes, _gameAssets.DefaultShoesSprites);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SetSprite(Image[] playerSpriteRenderers, Sprite[] defaultSprites)
        {
            for (int i = 0; i < defaultSprites.Length; i++)
            {
                playerSpriteRenderers[i].sprite = defaultSprites[i];
            }
        }

        private Sprite[] GetCurrentSprites(Image[] imageArray)
        {
            Sprite[] sprites = new Sprite[imageArray.Length];
            for (int i = 0; i < imageArray.Length; i++)
            {
                sprites[i] = imageArray[i].sprite;
            }
            return sprites;
        }

        private void LoadSavedCustomizationSprites()
        {
            SetSprite(hoodImages, _inventoryManager.GetEquippedSprites(ItemType.Hood) ?? _gameAssets.DefaultHoodSprites);
            SetSprite(armorImages, _inventoryManager.GetEquippedSprites(ItemType.Armor) ?? _gameAssets.DefaultArmorSprites);
            SetSprite(weaponImages, _inventoryManager.GetEquippedSprites(ItemType.Weapon) ?? _gameAssets.DefaultWeaponSprites);
            SetSprite(shoesImages, _inventoryManager.GetEquippedSprites(ItemType.Shoes) ?? _gameAssets.DefaultShoesSprites);
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PlaceItemInEquipmentSlotSignal>(OnPlaceItemInEquipmentSlot);
            _signalBus.Unsubscribe<PlaceItemInInventorySlotSignal>(OnPlaceItemInInventorySlot);
            _signalBus.Unsubscribe<PlayerCustomizationSignal>(OnPlayerCustomization);
            _signalBus.Unsubscribe<PlayerDefaultCustomizationSignal>(OnPlayerDefaultCustomization);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}