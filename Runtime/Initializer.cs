using Cysharp.Threading.Tasks;

namespace GameFramework
{
    public class Initializer : PersistentMonoSingleton<Initializer>
    {
        public static async UniTask InitializeForRuntime()
        {
            GameLog.Initialize();
            await TimeManager.Initialize();
            ConfigManager.Initialize();
            Data.Initialize();
            UIBindingManager.Initialize();
        }
    }
}