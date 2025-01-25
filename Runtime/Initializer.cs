using Cysharp.Threading.Tasks;

namespace GameFramework
{
    public class Initializer : PersistentMonoSingleton<Initializer>
    {
        public static async UniTask InitializeForRuntime()
        {
            var settings = InitializeSettings.GetOrAdd<InitializeSettings>();
            
            if(settings == null)
            {
                return;        
            }

            if (settings.IsGameLogEnabled) GameLog.Initialize();
            if (settings.IsTimeManagerEnabled) await TimeManager.Initialize();
            if (settings.IsConfigManagerEnabled) ConfigManager.Initialize();
#if USE_PUBSUB
            if (settings.IsUIBindManagerEnabled) UIBindingManager.Initialize();
#endif
        }
    }
}

/*
ETC Initialize
LiteDB.Initialize(Path)

*/