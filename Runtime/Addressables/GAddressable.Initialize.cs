#if USE_ADDRESSABLE
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFramework.Addressable
{
    public static partial class GAddressable
    {
        private static bool _isInitialized;

        private static async UniTask<IResourceLocator> InitializeAsyncInternal()
        {
            if (_isInitialized)
            {
                return null;
            }
            
            var result = await Addressables.InitializeAsync();  
            _isInitialized = true;
            GameLog.Debug("Percent Asset Initialized successfully.");
            return result;
        }

        private static async UniTask<IResourceLocator> InitializeAsyncInternal(string catalogPath)
        {
            if (_isInitialized)
            {
                return null;
            }

            await Addressables.InitializeAsync();
            var loadTask = Addressables.LoadContentCatalogAsync(catalogPath);  
            await loadTask.Task;

            if (loadTask.Status == AsyncOperationStatus.Failed)
            {
                GameLog.Error($"Failed to load content catalog: {catalogPath}");
            }
            
            _isInitialized = true;
            GameLog.Debug($"Percent Asset initialized successfully. Catalog path: {catalogPath}");
            return loadTask.Result;
        }

        private static void ValidateInitialize()
        {
            if (_isInitialized is false)
            {
                GameLog.Error("PercentAsset module is not initialized. Please call await PAsset.Initialize() first.");
            }
        }
    }
}
#endif