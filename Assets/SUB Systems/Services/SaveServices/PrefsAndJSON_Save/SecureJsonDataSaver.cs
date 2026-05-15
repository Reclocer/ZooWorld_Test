using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading;

#if CRYPTO
using System.Security.Cryptography;
using System.Text;
#endif

namespace SUBS.Core
{
    public static class SecureJsonDataSaver
    {
        private static readonly string encryptionKey = "5fXn#P2&$7LQcY9*";
        private static readonly string encryptionIV = "MyInitialization";

        private static AllData _data;
        public static AllData Data => _data;

        private static int _currentSaveSlot = 0;
        public static int CurrentSaveSlot
        {
            get => _currentSaveSlot;
            set => _currentSaveSlot = value;
        }

        private static readonly SemaphoreSlim _saveSemaphore = new SemaphoreSlim(1, 1);

        private static List<string> _reservedKeys = new List<string>
            {
                "userData",
                "DataTimeUnit",
                "audioLoopVolume",
                "audioMusicVolume",
                "audioVoiceVolume",
                "audioSoundVolume",
                "audioMasterVolume",
                "repositoryData",
            };

        public static void LoadData()
        {
            _data = new AllData();
            _data.Datas = new List<DataUnit>();

            string key = GetAllDataKey();
            string allData = PlayerPrefs.GetString(key, string.Empty);

            if (string.IsNullOrEmpty(allData))
            {
                Debug.Log("[SaveSystem] No save data found, starting fresh");
                return;
            }

            FileSizeChecker.CheckDataSize(allData, $"(Load slot {_currentSaveSlot})");

            try
            {
                var alldataJson = JsonConvert.DeserializeObject<AllData>(allData);
                if (alldataJson != null)
                {
                    _data = alldataJson;
                    Debug.Log("[SaveSystem] Data loaded successfully");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to deserialize data: {e.Message}");
                _data = new AllData();
                _data.Datas = new List<DataUnit>();
            }
        }

        public static void SaveEncryptedJsonData(string key, object data, JsonSerializerSettings settings = null)
        {
            try
            {
                string jsonData;

                if (settings == null)
                {
                    jsonData = JsonConvert.SerializeObject(data);
                }
                else
                {
                    jsonData = JsonConvert.SerializeObject(data, settings);
                }

#if CRYPTO
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                    aesAlg.IV = Encoding.UTF8.GetBytes(encryptionIV);

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);
                            csEncrypt.Write(jsonBytes, 0, jsonBytes.Length);
                            csEncrypt.FlushFinalBlock();
                        }

                        byte[] encryptedData = msEncrypt.ToArray();
                        SaveInBigData(key, Convert.ToBase64String(encryptedData));
                    }
                }
#else
                SaveInBigData(key, jsonData);
#endif
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error saving encrypted JSON data: {e.Message}\n{e.StackTrace}");
            }
        }

        public static T LoadEncryptedJsonData<T>(string key, JsonSerializerSettings settings = null)
        {
            try
            {
                string encryptedDataStringEditor = _data.Datas.FirstOrDefault(s => s.key == key)?.data;

                if (string.IsNullOrEmpty(encryptedDataStringEditor))
                {
                    Debug.LogWarning($"[SaveSystem] No data found for key: {key}");
                    return default(T);
                }

#if CRYPTO
                byte[] encryptedData = Convert.FromBase64String(encryptedDataStringEditor);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                    aesAlg.IV = Encoding.UTF8.GetBytes(encryptionIV);

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                string jsonData = srDecrypt.ReadToEnd();
                                return JsonConvert.DeserializeObject<T>(jsonData);
                            }
                        }
                    }
                }
#else
                if (settings == null)
                {
                    return JsonConvert.DeserializeObject<T>(encryptedDataStringEditor);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(encryptedDataStringEditor, settings);
                }
#endif
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error loading encrypted JSON data for key '{key}': {e.Message}");
                return default(T);
            }
        }

        private static void SaveInBigData(string key, string json)
        {
            try
            {                
                var allData = _data != null
                    ? CloneAllData(_data)
                    : new AllData { Datas = new List<DataUnit>() };

                var unit = new DataUnit
                {
                    data = json,
                    key = key
                };

                var existed = allData.Datas.FirstOrDefault(s => s.key == key);
                if (existed != null)
                {
                    existed.data = unit.data;
                }
                else
                {
                    allData.Datas.Add(unit);
                }
                                
                var dateUnit = new DataUnit
                {
                    data = DateTime.UtcNow.ToString("o"),
                    key = "DataTimeUnit"
                };

                var existedDate = allData.Datas.FirstOrDefault(s => s.key == "DataTimeUnit");
                if (existedDate != null)
                {
                    existedDate.data = dateUnit.data;
                }
                else
                {
                    allData.Datas.Add(dateUnit);
                }

                string serializedData = JsonConvert.SerializeObject(allData);

                if (!FileSizeChecker.CheckDataSize(serializedData, $"(SaveInBigData key: {key})"))
                {
                    Debug.LogError($"[SaveSystem] Data too large for key: {key}");
                    LogDataStatistics();
                    return;
                }

                _data = allData;

                CheckBaseAutoSafe();
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] SaveInBigData failed: {e.Message}\n{e.StackTrace}");
            }
        }

        private static AllData CloneAllData(AllData source)
        {
            if (source == null) return null;

            var clone = new AllData
            {
                Datas = new List<DataUnit>()
            };

            if (source.Datas != null)
            {
                foreach (var unit in source.Datas)
                {
                    clone.Datas.Add(new DataUnit
                    {
                        key = unit.key,
                        data = unit.data
                    });
                }
            }

            return clone;
        }

        public static void AutoSafeLogicInCurrentSlot()
        {
            // Fire and forget
            AutoSafeLogicInCustomSlotAsync(0).Forget();
        }

        public static void AutoSafeLogicInCustomSlot(int slot)
        {
            // Fire and forget
            AutoSafeLogicInCustomSlotAsync(slot).Forget();
        }

        public static async UniTask AutoSafeLogicInCustomSlotAsync(int slot)
        {
            await _saveSemaphore.WaitAsync();

            try
            {
                if (_data == null || _data.Datas == null)
                {
                    Debug.LogWarning("[SaveSystem] No data to save");
                    return;
                }

                await UniTask.SwitchToThreadPool();

                string serializedData;
                try
                {
                    serializedData = JsonConvert.SerializeObject(_data);
                }
                catch (Exception serializeEx)
                {
                    Debug.LogError($"[SaveSystem] Serialization failed: {serializeEx.Message}");
                    return;
                }

                await UniTask.SwitchToMainThread();

                if (!FileSizeChecker.CheckDataSize(serializedData, $"(AutoSave in slot {slot})"))
                {
                    Debug.LogError($"[SaveSystem] Save file too large for slot {slot}");
                    LogDataStatistics();
                    return;
                }

                try
                {
                    var testDeserialize = JsonConvert.DeserializeObject<AllData>(serializedData);
                    if (testDeserialize == null || testDeserialize.Datas == null)
                    {
                        Debug.LogError("[SaveSystem] Data validation failed");
                        return;
                    }
                }
                catch (Exception validateEx)
                {
                    Debug.LogError($"[SaveSystem] Data validation failed: {validateEx.Message}");
                    return;
                }

                string key = GetAllDataKeyInCustomSlot(slot);
                PlayerPrefs.SetString(key, serializedData);
                PlayerPrefs.Save();

                //Debug.Log($"[SaveSystem] Successfully saved to slot {slot}, size: {serializedData.Length / 1024f:F2} KB");

#if UNITY_STANDALONE
                await UniTask.SwitchToThreadPool();
                await SaveDataLocallyAsync(_data, slot);
                await UniTask.SwitchToMainThread();
#endif
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Critical error during save to slot {slot}: {e.Message}\n{e.StackTrace}");
            }
            finally
            {
                _saveSemaphore.Release();
            }
        }

        public static void UpdateDataFromSave(int id = 9)
        {
            _data = new AllData();
            _data.Datas = new List<DataUnit>();

            string key = GetAllDataKeyInCustomSlot(id);
            string allData = PlayerPrefs.GetString(key, string.Empty);

            if (string.IsNullOrEmpty(allData))
            {
                Debug.Log($"[SaveSystem] No data in slot {id}");
                return;
            }

            FileSizeChecker.CheckDataSize(allData, $"(Load slot {id})");

            try
            {
                var alldataJson = JsonConvert.DeserializeObject<AllData>(allData);
                if (alldataJson != null)
                {
                    _data = alldataJson;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to load slot {id}: {e.Message}");
            }
        }

        private static void CheckBaseAutoSafe()
        {
            AutoSafeLogicInCurrentSlot();
        }

        private static string GetAllDataKey()
        {
            return string.Format("{0}_{1}", PrefsKeys.allData, _currentSaveSlot.ToString());
        }

        private static string GetAllDataKeyInCustomSlot(int customSlot = 0)
        {
            return string.Format("{0}_{1}", PrefsKeys.allData, customSlot.ToString());
        }

        private static string GetFilePath(int slot)
        {
            return Path.Combine(Application.persistentDataPath, $"UserSaves{slot}.json");
        }

        private static async UniTask SaveDataLocallyAsync(AllData data, int slot = 0)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                string filePath = GetFilePath(slot);

                await File.WriteAllTextAsync(filePath, jsonData);

                Debug.Log($"[SaveSystem] Data saved locally at {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SaveSystem] Failed to save data locally: {ex.Message}");
            }
        }

        public static void SaveDataLocally(object data, int slot = 0)
        {
            SaveDataLocallyAsync((AllData)data, slot).Forget();
        }

        public static AllData LoadDataLocally<T>(int slot = 0)
        {
            try
            {
                string filePath = GetFilePath(slot);

                if (!File.Exists(filePath))
                {
                    Debug.Log($"[SaveSystem] Local file not found: {filePath}");
                    return null;
                }

                string jsonData = File.ReadAllText(filePath);
                AllData data = JsonConvert.DeserializeObject<AllData>(jsonData);
                Debug.Log("[SaveSystem] Data loaded from local file.");
                return data;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SaveSystem] Failed to load local data: {ex.Message}");
                return default;
            }
        }

        public static void LogDataStatistics()
        {
            if (_data == null || _data.Datas == null)
            {
                //Debug.Log("[SaveSystem] data = null");
                return;
            }

            FileSizeChecker.LogDataStatistics(_data.Datas, "Save file size");
        }

        public static List<(string key, float sizeKB)> GetLargestData(int topCount = 5)
        {
            if (_data == null || _data.Datas == null)
                return new List<(string, float)>();

            return FileSizeChecker.GetLargestDataUnits(_data.Datas, topCount);
        }

        public static bool CheckPrefsAndSave(string key, object data)
        {
            var dataUnit = _data.Datas.FirstOrDefault(s => s.key == key);

            if (dataUnit == null)
            {
                SaveEncryptedJsonData(key, data);
                return false;
            }

            return true;
        }

        public static AllData LoadEncrypt()
        {
            string encryptedDataString = PlayerPrefs.GetString(GetAllDataKeyInCustomSlot(0));

            if (encryptedDataString.Length == 0)
                return null;

            return _data;
        }

        public static List<AllData> LoadAllSlots(int slotsCount)
        {
            var list = new List<AllData>();

            for (int i = 0; i < slotsCount; i++)
            {
                string encryptedDataString = PlayerPrefs.GetString(GetAllDataKeyInCustomSlot(i));
                if (encryptedDataString.Length == 0)
                {
                    list.Add(new AllData());
                    continue;
                }

                try
                {
                    var unit = JsonConvert.DeserializeObject<AllData>(encryptedDataString);
                    list.Add(unit ?? new AllData());
                }
                catch
                {
                    list.Add(new AllData());
                }
            }

            return list;
        }

        public static AllData LoadSlot(int startLocSlotId)
        {
            string encryptedDataString = PlayerPrefs.GetString(GetAllDataKeyInCustomSlot(startLocSlotId));

            if (encryptedDataString.Length == 0)
            {
                return new AllData();
            }

            try
            {
                var data = JsonConvert.DeserializeObject<AllData>(encryptedDataString);
                return data ?? new AllData();
            }
            catch
            {
                return new AllData();
            }
        }

        public static void SaveEncrypt(string data)
        {
            if (!FileSizeChecker.CheckDataSize(data, "(SaveEncrypt in slot 0)"))
            {
                return;
            }

            PlayerPrefs.SetString(GetAllDataKeyInCustomSlot(0), data);
            PlayerPrefs.Save();
        }

        public static void SaveEncryptInNeededSlot(string data, int slot = 0)
        {
            if (!FileSizeChecker.CheckDataSize(data, $"(SaveEncrypt in slot {slot})"))
            {
                return;
            }

            PlayerPrefs.SetString(GetAllDataKeyInCustomSlot(slot), data);
            PlayerPrefs.Save();
        }

        public static void EraseDataProgress()
        {
            List<DataUnit> main = _data.Datas
                .Where(data => _reservedKeys.Contains(data.key))
                .ToList();

            _data.Datas = main;
            AutoSafeLogicInCurrentSlot();
        }
    }
}