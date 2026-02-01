using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    public class ConfigManager : IManager
    {
        private readonly string dicKey = $"{Application.identifier}.configs.gameframework";

        private Dictionary<string, object> _configs;

        public UniTask Initialize()
        {
            _configs = DictionaryPrefs.LoadDictionary<string, object>(dicKey);
            Debug.Log("[ConfigManager] Initialized");
            return UniTask.CompletedTask;
        }

        public void AddKey(string key, object value)
        {
            if (_configs.ContainsKey(key))
            {
                Debug.Log($"Already key. key: {key}");
                return;
            }

            _configs.Add(key, value);
        }

        public object GetConfig(string key)
        {
            if (_configs.TryGetValue(key, out var obj) is false)
            {
                Debug.Log($"Invalid key. key: {key}");
                return null;
            }

            return obj;
        }

        public void SetConfig(string key, object value)
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