using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    public class PopupManager : PersistentMonoSingleton<PopupManager>, IManager
    {
        private readonly Dictionary<Type, PopupBase> _instantiatedPopups = new Dictionary<Type, PopupBase>();
        [SerializeField] List<PopupBase> _popups = new List<PopupBase>();

        public UniTask Initialize()
        {
            Debug.Log("[PopupManager] Initialized");
            return UniTask.CompletedTask;
        }

        public T GetPopup<T>(bool isRefresh = false) where T : PopupBase
        {
            Type popupType = typeof(T);

            if (!_instantiatedPopups.TryGetValue(popupType, out var existing))
            {
                if (_popups.TryGetValueForType(popupType, out var prefab) is false)
                {
                    Debug.Log($"[PopupFactory] Could not find popup type {popupType.Name}");
                    return null;
                }

                var instance = (T)Instantiate(prefab, this.transform);
                _instantiatedPopups[popupType] = instance;
                (isRefresh ? (Action)instance.Refresh : instance.Open)();
                return instance;
            }

            var result = (T)existing;
            if (result.Active && isRefresh is false) return result;
            (isRefresh ? (Action)result.Refresh : result.Open)();
            return result;
        }

        public T GetPopup<T>(object data, bool isRefresh = false) where T : PopupBase
        {
            Type popupType = typeof(T);

            if (!_instantiatedPopups.TryGetValue(popupType, out var existing))
            {
                if (_popups.TryGetValueForType(popupType, out var prefab) is false)
                {
                    Debug.Log($"[PopupFactory] Could not find popup type {popupType.Name}");
                    return null;
                }

                var instance = (T)Instantiate(prefab, this.transform);
                _instantiatedPopups[popupType] = instance;
                (isRefresh ? (Action<object>)instance.Refresh : instance.Open)(data);
                return instance;
            }

            var result = (T)existing;
            if (result.Active && isRefresh is false) return result;
            (isRefresh ? (Action<object>)result.Refresh : result.Open)(data);
            return result;
        }
    }
}
