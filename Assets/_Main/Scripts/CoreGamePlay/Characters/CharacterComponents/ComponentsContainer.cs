using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class ComponentsContainer
    {
        [SerializeReference, ListDrawerSettings(DefaultExpandedState = true, ShowPaging = false)]
        private ICharacterComponent[] _components;
    }
}