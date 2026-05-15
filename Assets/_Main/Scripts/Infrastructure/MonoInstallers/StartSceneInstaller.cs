using Zenject;

public class StartSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
       // Container.Bind<ICursor>().To<Cursor_StartScene>().FromComponentInHierarchy().AsTransient();
    }
}
