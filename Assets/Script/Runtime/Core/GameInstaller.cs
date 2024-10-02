using Assets.Script.Runtime.AbilityModule;
using Assets.Script.Runtime.Signal;
using Script.Runtime.CollectableModule.ItemModule.Factory;
using Script.Runtime.InputModule;
using Script.Runtime.InventoryModule;
using Script.Runtime.Signal;
using Script.Runtime.SpawnerModule;
using Script.Runtime.UIModule;
using UnityEngine;
using Zenject;

namespace Script.Runtime.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private InputManagerConfig inputManagerConfig;
        
        [SerializeField]
        private ExpGemFactoryConfig expGemFactoryConfig;
        
        [SerializeField]
        private ItemFactoryConfig itemFactoryConfig;
        
        [SerializeField]
        private UIPanelManagerConfig uIPanelManagerConfig;
        
        [SerializeField]
        private ItemIconFactoryConfig itemIconFactoryConfig;
        
        [SerializeField]
        private GameAssetsConfig gameAssetsConfig;
        
        [SerializeField]
        private AbilityButtonManagerConfig abilityButtonManagerConfig;
        
        [SerializeField]
        private RpgAbilityConfig rpgAbilityConfig;

        [SerializeField]
        private GuardianAbilityConfig guardianAbilityConfig;

        [SerializeField]
        private PowerAreaAbilityConfig powerAreaAbilityConfig;

        [SerializeField]
        private EnemySpawnerConfig enemySpawnerConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIManager>().AsSingle();
            Container.BindInterfacesTo<InputManager>().AsSingle().WithArguments(inputManagerConfig);
            Container.BindInterfacesTo<ExpGemFactory>().AsSingle().WithArguments(expGemFactoryConfig);
            Container.BindInterfacesTo<ItemFactory>().AsSingle().WithArguments(itemFactoryConfig);
            Container.BindInterfacesAndSelfTo<InventoryManager>().AsSingle();
            Container.BindInterfacesTo<UIPanelManager>().AsSingle().WithArguments(uIPanelManagerConfig);
            Container.BindInterfacesTo<ItemIconFactory>().AsSingle().WithArguments(itemIconFactoryConfig);
            Container.Bind<GameAssets>().AsSingle().WithArguments(gameAssetsConfig);
            Container.BindInterfacesAndSelfTo<AbilityButtonManager>().AsSingle().WithArguments(abilityButtonManagerConfig);
            Container.BindInterfacesTo<RpgAbility>().AsSingle().WithArguments(rpgAbilityConfig);
            Container.BindInterfacesTo<GuardianAbility>().AsSingle().WithArguments(guardianAbilityConfig);
            Container.BindInterfacesTo<PowerAreaAbility>().AsSingle().WithArguments(powerAreaAbilityConfig);
            Container.BindInterfacesTo<EnemySpawner>().AsSingle().WithArguments(enemySpawnerConfig);

            BindingsSignal();
        }
        
        private void BindingsSignal()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<IncreasePlayerExpSignal>();
            Container.DeclareSignal<InstantiateExpGemSignal>();
            Container.DeclareSignal<InstantiateItemSignal>();
            Container.DeclareSignal<AddItemToItemListSignal>();
            Container.DeclareSignal<RemoveItemFromItemListSignal>();
            Container.DeclareSignal<ShowUIPanelSignal>();
            Container.DeclareSignal<HideUIPanelSignal>();
            Container.DeclareSignal<HideAllUIPanelsSignal>();
            Container.DeclareSignal<InstantiateItemIconSignal>();
            Container.DeclareSignal<PlaceItemInEquipmentSlotSignal>();
            Container.DeclareSignal<AddItemToEquipmentSlotListSignal>();
            Container.DeclareSignal<RemoveItemFromEquipmentSlotListSignal>();
            Container.DeclareSignal<PlaceItemInInventorySlotSignal>();
            Container.DeclareSignal<PlayerCustomizationSignal>();
            Container.DeclareSignal<PlayerDefaultCustomizationSignal>();
            Container.DeclareSignal<SetPlayerWeaponSignal>();
            Container.DeclareSignal<PlayerLevelUpSignal>();
            Container.DeclareSignal<InstantiateItemSignal>();
            Container.DeclareSignal<AbilityButtonClickSignal>();
            Container.DeclareSignal<UpdatePlayerExpUISignal>();
        }
    }
}