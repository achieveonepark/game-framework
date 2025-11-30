using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static partial class Core
    {
        public class Popup : PersistentMonoSingleton<Popup>
        {
            private List<PopupBase> _instantiatedPopup = new List<PopupBase>();
            [SerializeField] List<PopupBase> _popups = new List<PopupBase>();

            public static T GetPopup<T>(bool isRefresh = false) where T : PopupBase
            {
                Type popupType = typeof(T);

                if (Instance._popups.TryGetValueForType(popupType, out var popup) is false)
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
                    var instantiatedPopup = Instantiate(result, Instance.transform);
                    Instance._instantiatedPopup.Add(instantiatedPopup);

                    (isRefresh ? (Action)instantiatedPopup.Refresh : instantiatedPopup.Open)();
                    return instantiatedPopup;
                }

                (isRefresh ? (Action)result.Refresh : result.Open)();
                return result;
            }

            public static T GetPopup<T>(object data, bool isRefresh = false) where T : PopupBase
            {
                Type popupType = typeof(T);

                if (Instance._popups.TryGetValueForType(popupType, out var popup) is false)
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
                    var instantiatedPopup = Instantiate(result, Instance.transform);
                    Instance._instantiatedPopup.Add(instantiatedPopup);

                    (isRefresh ? (Action<object>)instantiatedPopup.Refresh : instantiatedPopup.Open)(data);
                    return instantiatedPopup;
                }

                (isRefresh ? (Action<object>)result.Refresh : result.Open)(data);
                return result;
            }

            private static bool IsInstantiated(PopupBase popupBase)
            {
                return Instance._instantiatedPopup.Contains(popupBase);
            }
        }
    }
}
