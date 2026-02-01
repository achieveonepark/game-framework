using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;
using UnityEngine.Pool;

namespace GameFramework
{
    public class PoolManager : IManager
    {
        private Dictionary<string, IObjectPool<GameObject>> _objs = new ();

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public void AddPool(string key, GameObject createObject)
        {
            // _objs.Add(key, new ObjectPool<GameObject>());
            // TODO: Implement actual pooling logic
        }

        public T GetObject<T>(string key) where T : Object
        {
            if (_objs.TryGetValue(key, out var pool) is false)
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