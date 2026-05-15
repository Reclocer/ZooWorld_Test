using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace SUBS.Core
{
    public static class EnumerableExtensions
    {
        #region List
        public static List<T> Shuffle<T>(this List<T> list)
        {            
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        /// <summary>
        /// Return list limited count the random selected items from list
        /// If list.Count <= limitCount => return list with out changes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="limitedCount"></param>
        /// <returns></returns>
        public static List<T> SelectRandomItems<T>(this List<T> list, int limitCount = 1)
        {
            if (list.Count == 0)
                return null;

            if (list.Count <= limitCount)
                return list;

            List<T> tempList = new List<T>(limitCount);
            int rd = 0;

            for (int i = 0; i < limitCount; i++)
            {
                rd = Random.Range(0, list.Count);

                if (!tempList.Contains(list[rd]))
                {
                    tempList.Add(list[rd]);
                }
                else
                {
                    i--;
                }
            }

            return tempList;
        }

        /// <summary>
        /// If the list of contains two or more instances of <T>, the last one will be removed.
        /// Example: for {2,3,2,4} => list.RemoveLastInstance<int>(2) result {2,3,4}
        /// </summary>
        public static void RemoveLastInstance<T>(this List<T> list, T instance)
        {
            if (list == null) 
                return;

            int lastIndex = list
                .Select((item, index) => new { item, index })
                .Where(x => EqualityComparer<T>.Default.Equals(x.item, instance))
                .Select(x => x.index)
                .LastOrDefault();

            if (lastIndex >= 0 && EqualityComparer<T>.Default.Equals(list[lastIndex], instance))
            {
                list.RemoveAt(lastIndex);
            }
        }
        #endregion List
    }
}