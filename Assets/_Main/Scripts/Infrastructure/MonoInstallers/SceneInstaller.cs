using SUBS.Core;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        //[SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            //Container.BindInstance(_gameConfig).AsSingle();

            //Container
            //.Bind(typeof(IService), typeof(ITicksService))
            //.To<TicksService>()
            //.FromComponentInHierarchy()
            //.AsSingle();
        }
    }
}
