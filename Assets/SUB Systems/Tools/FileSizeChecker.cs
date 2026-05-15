using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SUBS.Core
{
    public static class FileSizeChecker
    {
        public const int MAX_DATA_SIZE_KB = 800;
        public const int WARNING_DATA_SIZE_KB = 600;

        public static float GetDataSizeInKB(string data)
        {
            if (string.IsNullOrEmpty(data))
                return 0f;

            int byteCount = Encoding.UTF8.GetByteCount(data);
            return byteCount / 1024f;
        }

        public static int GetDataSizeInBytes(string data)
        {
            if (string.IsNullOrEmpty(data))
                return 0;

            return Encoding.UTF8.GetByteCount(data);
        }

        public static float GetDataSizeInMB(string data)
        {
            return GetDataSizeInKB(data) / 1024f;
        }
                
        public static bool CheckDataSize(string data, string context = "", int maxSizeKB = MAX_DATA_SIZE_KB, int warningSizeKB = WARNING_DATA_SIZE_KB)
        {
            float sizeKB = GetDataSizeInKB(data);

            if (sizeKB > maxSizeKB)
            {
                Debug.LogError($"[DataSizeChecker] File size > max size! {context}\n" +
                               $"Current size: {sizeKB:F2} KB\n" +
                               $"Max size: {maxSizeKB} KB\n" +
                               $"Up size: {(sizeKB - maxSizeKB):F2} KB");
                return false;
            }

            if (sizeKB > warningSizeKB)
            {
                Debug.Log($"[DataSizeChecker] Warning high file size! {context}\n" +
                          $"Current size: {sizeKB:F2} KB\n" +
                          $"Max size: {maxSizeKB} KB\n" +
                          $"Left until max size: {(maxSizeKB - sizeKB):F2} KB");
            }
            else
            {
                //Debug.Log($"[DataSizeChecker] File size: {sizeKB:F2} KB / {maxSizeKB} KB {context}");
            }

            return true;
        }

        public static string FormatSize(float sizeKB)
        {
            if (sizeKB < 1f)
            {
                return $"{(sizeKB * 1024f):F2} B";
            }
            else if (sizeKB < 1024f)
            {
                return $"{sizeKB:F2} KB";
            }
            else
            {
                return $"{(sizeKB / 1024f):F2} MB";
            }
        }

        public static void LogDataStatistics(List<DataUnit> dataUnits, string title = "Data size", int maxSizeKB = MAX_DATA_SIZE_KB)
        {
            if (dataUnits == null || dataUnits.Count == 0)
            {
                Debug.Log($"[DataSizeChecker] {title}: dataUnits.Count = null");
                return;
            }

            StringBuilder stats = new StringBuilder();
            stats.AppendLine($"[DataSizeChecker] {title}:");
            stats.AppendLine("─────────────────────────────────────");

            float totalSize = 0f;
            var sortedData = dataUnits.OrderByDescending(d => GetDataSizeInKB(d.data)).ToList();

            foreach (var dataUnit in sortedData)
            {
                float size = GetDataSizeInKB(dataUnit.data);
                totalSize += size;
                stats.AppendLine($"{dataUnit.key}: {FormatSize(size)}");
            }

            stats.AppendLine("─────────────────────────────────────");
            stats.AppendLine($"TOTAL: {FormatSize(totalSize)} / {maxSizeKB} KB");
            stats.AppendLine($"Used: {(totalSize / maxSizeKB * 100):F1}%");

            if (totalSize > maxSizeKB)
            {
                Debug.LogError(stats.ToString());
            }
            else if (totalSize > WARNING_DATA_SIZE_KB)
            {
                Debug.LogWarning(stats.ToString());
            }
            else
            {
                Debug.Log(stats.ToString());
            }
        }

        public static void LogDataStatistics(Dictionary<string, string> dataDictionary, string title = "Data size", int maxSizeKB = MAX_DATA_SIZE_KB)
        {
            if (dataDictionary == null || dataDictionary.Count == 0)
            {
                Debug.Log($"[DataSizeChecker] {title}: dataUnits.Count = null");
                return;
            }

            StringBuilder stats = new StringBuilder();
            stats.AppendLine($"[DataSizeChecker] {title}:");
            stats.AppendLine("─────────────────────────────────────");

            float totalSize = 0f;
            var sortedData = dataDictionary.OrderByDescending(kvp => GetDataSizeInKB(kvp.Value)).ToList();

            foreach (var kvp in sortedData)
            {
                float size = GetDataSizeInKB(kvp.Value);
                totalSize += size;
                stats.AppendLine($"{kvp.Key}: {FormatSize(size)}");
            }

            stats.AppendLine("─────────────────────────────────────");
            stats.AppendLine($"Total: {FormatSize(totalSize)} / {maxSizeKB} KB");
            stats.AppendLine($"Used: {(totalSize / maxSizeKB * 100):F1}%");

            if (totalSize > maxSizeKB)
            {
                Debug.LogError(stats.ToString());
            }
            else if (totalSize > WARNING_DATA_SIZE_KB)
            {
                Debug.LogWarning(stats.ToString());
            }
            else
            {
                Debug.Log(stats.ToString());
            }
        }

        public static List<(string key, float sizeKB)> GetLargestDataUnits(List<DataUnit> dataUnits, int topCount = 5)
        {
            if (dataUnits == null)
                return new List<(string, float)>();

            return dataUnits
                .Select(d => (d.key, GetDataSizeInKB(d.data)))
                .OrderByDescending(item => item.Item2)
                .Take(topCount)
                .ToList();
        }
    }
}