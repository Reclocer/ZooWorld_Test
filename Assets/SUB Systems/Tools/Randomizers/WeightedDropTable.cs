using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUBS.Core.Tools
{
    /// <summary>
    /// Usage sample:
    /// public class LootSpawner : MonoBehaviour
    ///{
    ///    [SerializeField] private WeightedDropTable lootTable;

    ///    public void Spawn()
    ///    {
    ///        var picked = lootTable.GetRandom();

    ///        if (picked is GameObject prefab)
    ///        {
    ///            Instantiate(prefab, transform.position, Quaternion.identity);
    ///        }
    ///    }
    ///}
    /// </summary>
    [Serializable]
    public class WeightedDropTable
    {
        [SerializeField] private List<ObjectWithChance> entries = new();

        public IReadOnlyList<ObjectWithChance> Entries => entries;

        public UnityEngine.Object GetRandom()
        {
            if (entries == null || entries.Count == 0)
            {
                Debug.LogError("Drop table is empty.");
                return null;
            }

            int total = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Target != null && entries[i].Chance > 0)
                    total += entries[i].Chance;
            }

            if (total <= 0)
            {
                Debug.LogError("Total chance must be greater than 0.");
                return null;
            }

            int roll = UnityEngine.Random.Range(0, total);
            int current = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];

                if (entry.Target == null || entry.Chance <= 0)
                    continue;

                current += entry.Chance;

                if (roll < current)
                    return entry.Target;
            }

            Debug.LogError("Random selection failed.");
            return null;
        }
    }
}
