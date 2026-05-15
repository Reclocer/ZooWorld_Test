using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SUBS.Core.EventBus;
using System;

namespace SUBS.Core
{
    public class ServicesInitializer : MonoBehaviour
    {
        [Inject]
        [SerializeReference, ReadOnly] private List<IService> _services;

        private void Start()
        {
            InitializeServices().Forget();
        }

        private async UniTask InitializeServices()
        {
            try
            {
                await UniTask.WhenAll(_services.Select(service => service.Init()));

                Debug.Log($"[{nameof(ServicesInitializer)}] All services initialized");
                SEventBus.Send(Constants.SERVICES_INITED);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
