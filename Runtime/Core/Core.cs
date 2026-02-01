using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine; // For Debug.LogWarning/LogError

namespace GameFramework
{
    public static partial class Core
    {
        private static readonly Dictionary<Type, IManager> s_managers = new Dictionary<Type, IManager>();

        /// <summary>
        /// 모든 기본 프레임워크 매니저들을 등록하고 초기화합니다.
        /// 게임 시작 시 한 번 호출되어야 합니다.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async UniTask InitializeAllManagers()
        {
            Debug.Log("[Core] Initializing all managers...");

            // Register non-MonoBehaviour managers
            await Register(new ConfigManager());
            await Register(new IAPManager());
            await Register(new InputManager());
            await Register(new PlayerManager());
            await Register(new SceneManager());
            await Register(new SoundManager());
            await Register(new TimeManager());

            // Special handling for MonoBehaviour-based singletons (e.g., PopupManager)
            // Assumes PopupManager.Instance is accessible (which PersistentMonoSingleton ensures)
            if (PopupManager.Instance != null)
            {
                 await Register(PopupManager.Instance);
            }
            else
            {
                 Debug.LogError("[Core] PopupManager.Instance is null. Is it set up correctly in the scene?");
            }

            Debug.Log("[Core] All managers initialized.");
        }

        /// <summary>
        /// 새로운 매니저를 등록하고 초기화합니다.
        /// </summary>
        public static async UniTask Register<T>(T manager) where T : IManager
        {
            Type type = typeof(T);
            if (s_managers.ContainsKey(type))
            {
                Debug.LogWarning($"[Core] Manager of type {type.Name} is already registered. Skipping.");
                return;
            }
            s_managers[type] = manager;
            await manager.Initialize(); // Initialize the manager right after registration
            Debug.Log($"[Core] Manager '{type.Name}' registered and initialized.");
        }

        /// <summary>
        /// 등록된 매니저를 타입으로 가져옵니다.
        /// </summary>
        public static T Get<T>() where T : class, IManager
        {
            if (s_managers.TryGetValue(typeof(T), out IManager manager))
            {
                return manager as T;
            }
            Debug.LogError($"[Core] Manager of type {typeof(T).Name} is not registered. Returning null.");
            return null;
        }
    }
}