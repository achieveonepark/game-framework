using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class Initializer : PersistentMonoSingleton<Initializer>
    {
        [SerializeField] private InitializeSettings _settings;
        private static InitializeSettings _initializeSettings => Instance._settings;
        
        public static async UniTask InitializeForRuntime()
        {
            GameLog.Initialize();
            // await TimeManager.Initialize();
            // Data.SetDB(_initializeSettings.DBPath);
            UIBindingManager.Initialize();
        }
    }
}