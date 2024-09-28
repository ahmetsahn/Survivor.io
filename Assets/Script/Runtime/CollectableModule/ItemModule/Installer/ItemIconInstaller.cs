using Script.Ahmet.StateMachine;
using Script.Runtime.CollectableModule.ItemModule.Controller;
using Script.Runtime.CollectableModule.ItemModule.State.ItemIconState;
using Script.Runtime.CollectableModule.ItemModule.View;
using UnityEngine;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.Installer
{
    public class ItemIconInstaller : MonoInstaller
    {
        [SerializeField]
        private ItemIconOnInventoryStateConfig itemIconOnInventoryStateConfig;
        
        [SerializeField]
        private ItemIconEquippedStateConfig itemIconEquippedStateConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<ItemIconView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StateMachine>().AsSingle();
            Container.BindInterfacesTo<ItemIconStateMachineController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemIconOnInventoryState>().AsSingle().WithArguments(itemIconOnInventoryStateConfig);
            Container.BindInterfacesAndSelfTo<ItemIconEquippedState>().AsSingle().WithArguments(itemIconEquippedStateConfig);
        }
    }
}