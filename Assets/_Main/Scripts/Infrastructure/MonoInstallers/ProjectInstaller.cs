using SUBS.Core;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.BindInterfacesAndSelfTo<SLogger>()
            //    .FromNew()
            //    .AsSingle()
            //    .NonLazy();

            Container.BindInterfacesAndSelfTo<ApplicationPipeline>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            //Container
            //    .Bind(typeof(IService), typeof(IInputService))
            //    .To<InputService>()
            //    .FromComponentInHierarchy()
            //    .AsSingle();

            //Container.Bind<IService>()
            //    .To<LoadingSceneService>()
            //    .FromComponentsInHierarchy()
            //    .AsTransient();


            //Container
            //    .Bind(typeof(IService), typeof(IWalletsService))
            //    .To<WalletSystem>()
            //    .FromComponentInHierarchy()
            //    .AsSingle();

            //Container
            //    .Bind(typeof(IService), typeof(IAdsService))
            //    .To<AdsService>()
            //    .FromNew()
            //    .AsSingle();

            //Container.Bind<LoadingSceneService>()
            //    .FromComponentInHierarchy()
            //    .AsSingle();
        }
    }
}
