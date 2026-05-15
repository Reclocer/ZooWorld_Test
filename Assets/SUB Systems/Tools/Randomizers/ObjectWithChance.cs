using System;
using UnityEngine;

namespace SUBS.Core.Tools
{
    [Serializable]
    public struct ObjectWithChance
    {
        [SerializeField] private UnityEngine.Object target;
        [SerializeField] private int chance;

        public UnityEngine.Object Target => target;

        public int Chance
        {
            get => chance;
            set => chance = ClampChance(value);
        }

        public ObjectWithChance(UnityEngine.Object target, int chance)
        {
            this.target = target;
            this.chance = 0;
            this.chance = ClampChance(chance);
        }

        private static int ClampChance(int value)
        {
            if (value < 0)
            {
                Debug.LogError("Chance cannot be less than 0. Value set to 0.");
                return 0;
            }

            if (value > 100)
            {
                Debug.LogError("Chance cannot be greater than 100. Value set to 100.");
                return 100;
            }

            return value;
        }
    }
}
