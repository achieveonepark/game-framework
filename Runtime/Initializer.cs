using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class Initializer : PersistentMonoSingleton<Initializer>
    {
        public static async UniTask InitializeForRuntime()
        {
            var settings = GScriptableObject.GetOrAdd<InitializeSettings>();

            ConfigManager.Initialize();
            GameLog.Initialize();
            await TimeManager.Initialize();
            Data.SetDB($"{settings.DBPath}");
            UIBindingManager.Initialize();
        }
    }
}