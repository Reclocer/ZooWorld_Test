using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace SUBS.Core.Tools
{
    public static class Randomizers
    {
        /// <summary>
        /// Execute(call) action with chance 0-100%
        /// </summary>
        public static bool Execute_WithChancePercent(ref float chancePercent, Action onSuccess)
        {
            if (chancePercent <= 0)
            {
                return false;
            }
            else if (chancePercent >= 100)
            {
                onSuccess.Invoke();
                return true;
            }

            float randomValue = Random.Range(0f, 100f);

            if (chancePercent >= randomValue)
            {
                onSuccess.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Call onSuccess with chance 0-100%, else call onFailure
        /// </summary>
        public static bool Execute_WithChancePercent(ref float chancePercent, Action onSuccess, Action onFailure)
        {
            if (Execute_WithChancePercent(ref chancePercent, onSuccess))
            {
                return true;
            }
            else
            {
                onFailure.Invoke();
                return false;
            }
        }

        /// <summary>
        /// Usage sample:
        /// obj = Randomizers.ChoseRandomWithChance(
        ///    new ObjectWithChance(gun, 50),
        ///    new ObjectWithChance(shield, 30),
        ///    new ObjectWithChance(missile, 20)
        ///);
        /// </summary>
        public static Object ChoseRandomObject_WithChance(params ObjectWithChance[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                Debug.LogError("No objects provided.");
                return null;
            }

            int totalChance = 0;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].Chance > 0 && objects[i].Target != null)
                    totalChance += objects[i].Chance;
            }

            if (totalChance <= 0)
            {
                Debug.LogError("Total chance must be greater than 0.");
                return null;
            }

            int roll = Random.Range(0, totalChance);
            int current = 0;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].Chance <= 0 || objects[i].Target == null)
                    continue;

                current += objects[i].Chance;

                if (roll < current)
                    return objects[i].Target;
            }

            Debug.LogError("Random selection failed.");
            return null;
        }
    }
}
