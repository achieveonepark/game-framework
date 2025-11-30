using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GameFramework
{
    public static partial class Core
    {
        public class Pool
        {
            private static Pool _instance;
            private static Pool getInstance => _instance ??= new Pool();
            private Dictionary<string, IObjectPool<GameObject>> _objs = new ();

            public static void AddPool(string key, GameObject createObject)
            {
                // _objs.Add(key, new ObjectPool<GameObject>());
            }

            public static T GetObject<T>(string key) where T : Object
            {
                if (getInstance._objs.TryGetValue(key, out var pool) is false)
                {
                    return null;
                }

                var obj = pool.Get();

                if (obj.TryGetComponent<T>(out var component) is false)
                {
                    return null;
                }

                return component;
            }
        }
    }
}