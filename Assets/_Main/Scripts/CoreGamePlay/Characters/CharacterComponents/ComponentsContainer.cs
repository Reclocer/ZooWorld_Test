using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class ComponentsContainer
    {        
        [SerializeReference, ListDrawerSettings(DefaultExpandedState = true, ShowPaging = false)]
        [OnCollectionChanged(nameof(OnComponentsCollectionChanged))]
        private ICharacterComponent[] _components = Array.Empty<ICharacterComponent>();

        public IReadOnlyList<ICharacterComponent> All => _components ?? Array.Empty<ICharacterComponent>();
        public event Action OnChanged;

        public T Get<T>() where T : class, ICharacterComponent
        {
            foreach (ICharacterComponent component in All)
            {
                if (component is T typed)
                    return typed;
            }

            return null;
        }

        public void Initialize(ICharacter character)
        {
            foreach (ICharacterComponent component in All)
            {
                component.Init(character);
            }
        }

        public void Tick(float fixedDeltaTime)
        {
            foreach (ICharacterComponent component in All)
            {
                if (component is ICharacterTickable tickable)
                    tickable.Tick(fixedDeltaTime);
            }
        }

        public List<DerivedCharacterTraitEntry> CollectTraitEntries()
        {
            List<DerivedCharacterTraitEntry> entries = new();
            HashSet<CharacterTrait> seen = new();

            foreach (ICharacterComponent component in All)
            {
                if (component is not ICharacterTraitProvider provider)
                    continue;

                string source = component.GetType().Name;

                foreach (CharacterTrait trait in provider.GetTraits())
                {
                    if (trait == CharacterTrait.None || !seen.Add(trait))
                        continue;

                    entries.Add(new DerivedCharacterTraitEntry(trait, source));
                }
            }

            entries.Sort((a, b) => a.Trait.CompareTo(b.Trait));
            return entries;
        }

        private void OnComponentsCollectionChanged()
        {
            OnChanged?.Invoke();
        }
    }
}
