using Cysharp.Threading.Tasks;
using SUBS.Core.ResourceManagement;
using Zenject;

public sealed class ApplicationPipeline : IInitializable
{
    public void Initialize()
    {
        LoadConfig().Forget();
    }

    private async UniTaskVoid LoadConfig()
    {
        ApplicationSettingsConfig applicationSettingsConfig = await
            AddressableObjectsLoader.LoadAssetAsync<ApplicationSettingsConfig>(nameof(ApplicationSettingsConfig));

        applicationSettingsConfig.Apply();
    }
}
