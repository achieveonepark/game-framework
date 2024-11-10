#if USE_ADDRESSABLE
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFramework.Addressable
{
    public static partial class GAddressable 
    {
        private static async UniTask<bool> PreloadAllAssetsInternal(string label, bool autoRelease = true)
        {
            ValidateInitialize();

            var handle = Addressables.DownloadDependenciesAsync(label, autoRelease);
            await handle.Task;
            return handle.Status == AsyncOperationStatus.Succeeded;
        }

        private static async UniTask UpdateResourceAsyncInternal(string label)
        {
            ValidateInitialize();

            var list = await Addressables.CheckForCatalogUpdates();

            if (list == null || list.Count == 0)
            {
                return;
            }
            
            var updateList = await Addressables.UpdateCatalogs(list, true);

            if (updateList == null || updateList.Count == 0)
            {
                return;
            }
            
            foreach (var res in updateList)
            {
                var downloadHandle = Addressables.DownloadDependenciesAsync(res, true);
                await downloadHandle.Task;

                if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameLog.Debug($"Successfully updated catalog: {res}");
                }
            }
        }
    }
}
#endif