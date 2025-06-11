using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 옵션에 사용 될 데이터들을 가볍게 PlayerPref에 적재합니다.
    /// </summary>
    public static class ConfigManager
    {
        private static readonly string dicKey = $"{Application.identifier}.configs.gameframework";
        
        private static Dictionary<string, object> _configs;

        internal static void Initialize()
        {
            _configs = DictionaryPrefs.LoadDictionary<string, object>(dicKey);
            GameLog.Debug("[ConfigManager] Initialized");
        }
        
        public static void AddKey(string key, object value)
        {
            if (_configs.ContainsKey(key))
            {
                Debug.Log($"Already key. key: {key}");
                return;
            }

            _configs.Add(key, value);
        }

        public static object GetConfig(string key)
        {
            if (_configs.TryGetValue(key, out var obj) is false)
            {
                Debug.Log($"Invalid key. key: {key}");
                return null;
            }

            return obj;
        }

        public static void SetConfig(string key, object value)
        {
            if (!_configs.ContainsKey(key))
            {
                Debug.Log($"Invalid key. key: {key}");
                return;
            }   
            
            _configs[key] = value;
            
            DictionaryPrefs.SaveDictionary(dicKey, _configs);
        }
    }
}