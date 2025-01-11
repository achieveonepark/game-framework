#if USE_PUBSUB

using System;
using System.Collections.Concurrent;
using System.Threading;
using Cysharp.Threading.Tasks;

using UniTaskPubSub;


namespace GameFramework
{
    public class UIBindingManager
    {
        private static UIBindingManager _instance;
        private static readonly object _lock = new();
        private ConcurrentDictionary<Type, AsyncMessageBus> _asyncMessageBus;

        public static void Initialize()
        {
            lock (_lock)
            {
                _instance = new UIBindingManager();
                _instance._asyncMessageBus = new ConcurrentDictionary<Type, AsyncMessageBus>();
                GameLog.Debug("[UIBindingManager] Initialized");
            }
        }

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

        public static AsyncMessageBus GetOrCreate<T>() where T : class
        {
            if (_instance == null)
            {
                Initialize();
            }
            
            Type type = typeof(T);

            if (_instance._asyncMessageBus.TryGetValue(type, out var bus) is false)
            {
                bus = new AsyncMessageBus();
                _instance._asyncMessageBus.GetOrAdd(type, bus);
                return bus;
            }

            return bus;
        }
        
        public static void ClearAll()
        {
            if (_instance == null)
            {
                Initialize();
                return;
            }
            
            _instance._asyncMessageBus.Clear();
        }

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