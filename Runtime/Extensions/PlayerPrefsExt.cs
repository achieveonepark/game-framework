using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace GameFramework
{
    public static class DictionaryPrefs
    {
        public static void SaveDictionary<TKey, TValue>(string key, Dictionary<TKey, TValue> dictionary)
        {
            string json = JsonConvert.SerializeObject(dictionary);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        
        public static Dictionary<TKey, TValue> LoadDictionary<TKey, TValue>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
                return new Dictionary<TKey, TValue>();

            string json = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
        }
    }
}