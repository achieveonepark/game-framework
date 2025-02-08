using System.Collections.Generic;
using Unity.Serialization.Json;
using UnityEngine;

namespace GameFramework
{
    public static class DictionaryPrefs
    {
        public static void SaveDictionary<TKey, TValue>(string key, Dictionary<TKey, TValue> dictionary)
        {
            string json = JsonSerialization.ToJson(dictionary);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        
        public static Dictionary<TKey, TValue> LoadDictionary<TKey, TValue>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
                return new Dictionary<TKey, TValue>();

            var json = PlayerPrefs.GetString(key);
            return JsonSerialization.FromJson<Dictionary<TKey, TValue>>(json);
        }
    }
}