using Assets.Script.Runtime.PlayerModule.Controller;
using Script.Runtime.PlayerModule.Controller;
using Script.Runtime.PlayerModule.Model;
using Script.Runtime.PlayerModule.View;
using UnityEngine;
using Zenject;

namespace Script.Runtime.PlayerModule.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerConfig playerConfig;
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerModel>().AsSingle().WithArguments(playerConfig);
            Container.BindInterfacesTo<PlayerHealthController>().AsSingle();
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
            Container.BindInterfacesTo<PlayerWeaponAimController>().AsSingle();
            Container.BindInterfacesTo<PlayerAttackController>().AsSingle();
            Container.BindInterfacesTo<PlayerInteractionController>().AsSingle();
            Container.BindInterfacesTo<PlayerCustomizationController>().AsSingle();
            Container.BindInterfacesTo<PlayerExpController>().AsSingle();
            Container.BindInterfacesTo<PlayerAnimationController>().AsSingle();
        }
    }
}