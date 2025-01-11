// using Achieve.Database;
using Cysharp.Threading.Tasks;

namespace GameFramework
{
    public class Initializer : PersistentMonoSingleton<Initializer>
    {
        public static async UniTask InitializeForRuntime()
        {
            var settings = InitializeSettings.GetOrAdd<InitializeSettings>();
            
            GameLog.Initialize();
            await TimeManager.Initialize();
            ConfigManager.Initialize();
            // LiteDB.Initialize(settings.DBPath);
#if USE_PUBSUB
            UIBindingManager.Initialize();
#endif
        }
    }
}