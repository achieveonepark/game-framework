#if USE_PUBSUB

using System;
using System.Collections.Concurrent;
using System.Threading;
using Cysharp.Threading.Tasks;

using UniTaskPubSub;


namespace GameFramework
{
    /// <summary>
    /// UI 데이터 바인딩을 위한 메시지 버스(Message Bus) 매니저입니다.
    /// USE_PUBSUB 심볼이 정의된 경우에만 컴파일됩니다.
    /// 메시지 타입을 기반으로 발행(Publish)/구독(Subscribe) 패턴을 제공합니다.
    /// 이 패턴을 사용하면 UI와 게임 로직 사이의 직접 참조 없이 데이터를 전달할 수 있습니다.
    /// 예: 점수 변경 시 ScoreChangedMessage를 발행하면, 점수 UI가 이를 구독해 자동으로 갱신됩니다.
    /// </summary>
    public class UIBindingManager
    {
        // 싱글톤 인스턴스
        private static UIBindingManager _instance;
        // 멀티스레드 환경에서 인스턴스 생성 안전성을 위한 잠금 객체
        private static readonly object _lock = new();
        // 타입별로 메시지 버스를 저장하는 스레드 안전한 딕셔너리
        private ConcurrentDictionary<Type, AsyncMessageBus> _asyncMessageBus;

        /// <summary>
        /// 게임 시작 시 자동으로 UIBindingManager를 초기화합니다.
        /// [RuntimeInitializeOnLoadMethod]는 씬 로드 없이도 게임 시작 시 자동 호출됩니다.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            lock (_lock)
            {
                // 스레드 안전하게 인스턴스를 생성하고 메시지 버스 딕셔너리를 초기화합니다
                _instance = new UIBindingManager();
                _instance._asyncMessageBus = new ConcurrentDictionary<Type, AsyncMessageBus>();
                GameLog.Debug("[UIBindingManager] Initialized");
            }
        }

        /// <summary>
        /// 특정 메시지 타입을 구독합니다.
        /// 해당 타입의 메시지가 발행(Publish)될 때마다 callback이 호출됩니다.
        /// </summary>
        /// <typeparam name="T">구독할 메시지 타입</typeparam>
        /// <param name="callback">메시지가 발행되면 호출할 콜백 함수</param>
        public static void Subscribe<T>(Action<T> callback) where T : class
        {
            var messageBus = GetOrCreate<T>();

            if (messageBus == null)
            {
                GameLog.Warning($"invalid {typeof(T).Name}");
                return;
            }

            messageBus.Subscribe<T>(callback);
        }

        /// <summary>
        /// 특정 메시지를 비동기로 발행합니다.
        /// 구독자들이 메시지를 처리할 때까지 await으로 기다릴 수 있습니다.
        /// </summary>
        /// <typeparam name="T">발행할 메시지 타입</typeparam>
        /// <param name="msg">발행할 메시지 객체</param>
        /// <param name="cancellation">취소 토큰. 기본값은 취소 없음.</param>
        public static async UniTask PublishAsync<T>(T msg, CancellationToken cancellation = default) where T : class
        {
            var messageBus = GetOrCreate<T>();

            if (messageBus == null)
            {
                GameLog.Warning($"invalid {typeof(T).Name}");
                return;
            }

            await messageBus.PublishAsync(msg, cancellation);
        }

        /// <summary>
        /// 특정 메시지를 동기적으로 발행합니다.
        /// 구독자들에게 즉시 메시지를 전달하지만 완료를 기다리지 않습니다.
        /// </summary>
        /// <typeparam name="T">발행할 메시지 타입</typeparam>
        /// <param name="msg">발행할 메시지 객체</param>
        /// <param name="cancellation">취소 토큰</param>
        public static void Publish<T>(T msg, CancellationToken cancellation = default) where T : class
        {
            var messageBus = GetOrCreate<T>();

            if (messageBus == null)
            {
                GameLog.Warning($"invalid {typeof(T).Name}");
                return;
            }

            messageBus.Publish(msg, cancellation);
        }

        /// <summary>
        /// 특정 메시지 타입에 해당하는 메시지 버스를 가져오거나 없으면 새로 생성합니다.
        /// 인스턴스가 없으면 자동으로 초기화합니다.
        /// </summary>
        /// <typeparam name="T">메시지 버스를 찾을 메시지 타입</typeparam>
        /// <returns>해당 타입의 AsyncMessageBus 인스턴스</returns>
        public static AsyncMessageBus GetOrCreate<T>() where T : class
        {
            if (_instance == null)
            {
                Initialize();
            }

            Type type = typeof(T);

            // 이미 해당 타입의 버스가 있으면 반환, 없으면 새로 생성합니다
            if (_instance._asyncMessageBus.TryGetValue(type, out var bus) is false)
            {
                bus = new AsyncMessageBus();
                // GetOrAdd는 스레드 안전하게 추가합니다 (동시 접근 시 중복 생성 방지)
                _instance._asyncMessageBus.GetOrAdd(type, bus);
                return bus;
            }

            return bus;
        }

        /// <summary>
        /// 등록된 모든 메시지 버스를 초기화합니다.
        /// 씬 전환 시 UI 구독을 모두 정리할 때 사용합니다.
        /// </summary>
        public static void ClearAll()
        {
            if (_instance == null)
            {
                Initialize();
                return;
            }

            _instance._asyncMessageBus.Clear();
        }

        /// <summary>
        /// 특정 메시지 타입의 버스가 등록되어 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">확인할 메시지 타입</typeparam>
        /// <returns>해당 타입의 버스가 있으면 true</returns>
        public static bool Contains<T>() where T : class
        {
            if (_instance == null)
            {
                Initialize();
                return false;
            }

            Type type = typeof(T);
            return _instance._asyncMessageBus.ContainsKey(type);
        }
    }
}

#endif
