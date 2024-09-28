using Script.Runtime.EnemyModule.Controller;
using Script.Runtime.EnemyModule.Model;
using Script.Runtime.EnemyModule.View;
using UnityEngine;
using Zenject;

namespace Script.Runtime.EnemyModule.Installer
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private EnemySo data;
        public override void InstallBindings()
        {
            Container.Bind<EnemyView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyModel>().AsSingle().WithArguments(data);
            Container.BindInterfacesTo<EnemyMovementController>().AsSingle();
            Container.BindInterfacesTo<EnemyAttackController>().AsSingle();
            Container.BindInterfacesTo<EnemyHealthController>().AsSingle();
        }
    }
}