using System;
using Script.Runtime.Enum;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.PlayerModule.View;
using Script.Runtime.Signal;
using UnityEngine;
using Zenject;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerCustomizationController : IDisposable
    {
        private readonly PlayerView _view;
        
        private readonly PlayerModel _model;
        
        private readonly SignalBus _signalBus;
        
        private readonly GameAssets _gameAssets;
        
        public PlayerCustomizationController(PlayerView view, PlayerModel model, SignalBus signalBus, GameAssets gameAssets)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            _gameAssets = gameAssets;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PlayerCustomizationSignal>(PlayerCustomization);
            _signalBus.Subscribe<PlayerDefaultCustomizationSignal>(PlayerDefaultCustomization);
        }
        
        private void PlayerCustomization(PlayerCustomizationSignal signal)
        {
            switch (signal.ItemData.ItemType)
            {
                case ItemType.Hood:
                    signal.ItemData.Customization(_view.HoodRenderers);
                    break;
                case ItemType.Armor:
                    signal.ItemData.Customization(_view.ArmorRenderers);
                    break;
                case ItemType.Weapon:
                    signal.ItemData.Customization(_view.WeaponRenderers);
                    _signalBus.Fire(new SetPlayerWeaponSignal(signal.ItemData.GetWeaponSo()));
                    break;
                case ItemType.Shoes:
                    signal.ItemData.Customization(_view.ShoesRenderers);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void PlayerDefaultCustomization(PlayerDefaultCustomizationSignal signal)
        {
            switch (signal.ItemType)
            {
                case ItemType.Hood:
                    SetSprite(_view.HoodRenderers, _gameAssets.DefaultHoodSprites);
                    break;
                case ItemType.Armor:
                    SetSprite(_view.ArmorRenderers, _gameAssets.DefaultArmorSprites);
                    break;
                case ItemType.Weapon:
                    SetSprite(_view.WeaponRenderers, _gameAssets.DefaultWeaponSprites);
                    _signalBus.Fire(new SetPlayerWeaponSignal(_model.DefaultWeapon));
                    break;
                case ItemType.Shoes:
                    SetSprite(_view.ShoesRenderers, _gameAssets.DefaultShoesSprites);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SetSprite(SpriteRenderer[] playerSpriteRenderers, Sprite[] defaultSprites)
        {
            for (int i = 0; i < playerSpriteRenderers.Length; i++)
            {
                playerSpriteRenderers[i].sprite = defaultSprites[i];
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PlayerCustomizationSignal>(PlayerCustomization);
            _signalBus.Unsubscribe<PlayerDefaultCustomizationSignal>(PlayerDefaultCustomization);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}