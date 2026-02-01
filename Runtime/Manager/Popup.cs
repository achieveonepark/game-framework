using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    public class PopupManager : PersistentMonoSingleton<PopupManager>, IManager
    {
        private List<PopupBase> _instantiatedPopup = new List<PopupBase>();
        [SerializeField] List<PopupBase> _popups = new List<PopupBase>(); // Assuming TryGetValueForType is an extension for List<PopupBase>

        public UniTask Initialize()
        {
            // The PersistentMonoSingleton's Awake handles its own initialization.
            // We can add specific manager setup here if needed.
            Debug.Log("[PopupManager] Initialized");
            return UniTask.CompletedTask;
        }

        public T GetPopup<T>(bool isRefresh = false) where T : PopupBase
        {
            Type popupType = typeof(T);

            // Assuming TryGetValueForType is an extension method somewhere
            if (this._popups.TryGetValueForType(popupType, out var popup) is false)
            {
                Debug.Log($"[PopupFactory] Could not find popup type {popupType.Name}");
                return null;
            }

            var result = (T)popup;

            if (result.Active && isRefresh is false)
            {
                return result;
            }

            if (IsInstantiated(result) is false)
            {
                var instantiatedPopup = Instantiate(result, this.transform);
                this._instantiatedPopup.Add(instantiatedPopup);

                (isRefresh ? (Action)instantiatedPopup.Refresh : instantiatedPopup.Open)();
                return instantiatedPopup;
            }

            (isRefresh ? (Action)result.Refresh : result.Open)();
            return result;
        }

        public T GetPopup<T>(object data, bool isRefresh = false) where T : PopupBase
        {
            Type popupType = typeof(T);

            if (this._popups.TryGetValueForType(popupType, out var popup) is false)
            {
                Debug.Log($"[PopupFactory] Could not find popup type {popupType.Name}");
                return null;
            }

            var result = (T)popup;

            if (result.Active && isRefresh is false)
            {
                return result;
            }

            if (IsInstantiated(result) is false)
            {
                var instantiatedPopup = Instantiate(result, this.transform);
                this._instantiatedPopup.Add(instantiatedPopup);

                (isRefresh ? (Action<object>)instantiatedPopup.Refresh : instantiatedPopup.Open)(data);
                return instantiatedPopup;
            }

            (isRefresh ? (Action<object>)result.Refresh : result.Open)(data);
            return result;
        }

        private bool IsInstantiated(PopupBase popupBase)
        {
            return this._instantiatedPopup.Contains(popupBase);
        }
    }
}
