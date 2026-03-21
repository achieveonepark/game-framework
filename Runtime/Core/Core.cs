using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine; // For Debug.LogWarning/LogError

namespace GameFramework
{
    /// <summary>
    /// 게임 프레임워크의 핵심 클래스입니다.
    /// 모든 매니저(Manager)를 등록하고 관리하는 중앙 허브 역할을 합니다.
    /// partial 키워드를 사용해 여러 파일로 분리할 수 있습니다.
    /// </summary>
    public static partial class Core
    {
        // 타입(Type)을 키로, 매니저 인스턴스를 값으로 저장하는 딕셔너리
        // 각 매니저는 타입 하나당 하나만 등록됩니다 (중복 등록 방지)
        private static readonly Dictionary<Type, IManager> s_managers = new Dictionary<Type, IManager>();

        /// <summary>
        /// 모든 기본 프레임워크 매니저들을 등록하고 초기화합니다.
        /// 게임 시작 시 한 번 호출되어야 합니다.
        /// </summary>
        /// <remarks>
        /// [RuntimeInitializeOnLoadMethod] 는 Unity에서 제공하는 특별한 어트리뷰트입니다.
        /// BeforeSceneLoad 옵션을 사용하면 첫 씬이 로드되기 전에 자동으로 이 메서드가 실행됩니다.
        /// 즉, 별도로 호출하지 않아도 게임 실행 시 자동으로 모든 매니저가 초기화됩니다.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad() => InitializeAllManagers().Forget();

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
        /// 이미 같은 타입의 매니저가 등록되어 있으면 경고를 출력하고 건너뜁니다.
        /// </summary>
        /// <typeparam name="T">등록할 매니저의 타입. IManager 인터페이스를 구현해야 합니다.</typeparam>
        /// <param name="manager">등록할 매니저 인스턴스</param>
        public static async UniTask Register<T>(T manager) where T : IManager
        {
            Type type = typeof(T);
            // 이미 같은 타입의 매니저가 있으면 중복 등록하지 않음
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
        /// 예: Core.Get&lt;SoundManager&gt;() 처럼 사용하면 사운드 매니저를 가져올 수 있습니다.
        /// 등록되지 않은 타입을 요청하면 null을 반환하고 에러 로그를 출력합니다.
        /// </summary>
        /// <typeparam name="T">가져올 매니저의 타입</typeparam>
        /// <returns>등록된 매니저 인스턴스, 없으면 null</returns>
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
