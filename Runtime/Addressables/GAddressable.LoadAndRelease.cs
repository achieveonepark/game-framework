#if USE_ADDRESSABLE
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace GameFramework.Addressable
{
    public static partial class GAddressable 
    {
        private static async UniTask<ResourceCache> LoadResourcesAsyncInternal(string label)
        {
            ValidateInitialize();
            
            var locationHandle = Addressables.LoadResourceLocationsAsync(label);
            await locationHandle.Task;

            if (locationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                GameLog.Error($"Failed to load label: {label}");
            }
            
            var locations = locationHandle.Result;
            
            var loadHandle = Addressables.LoadAssetsAsync<Object>(locations, null);
            await loadHandle.Task;

            if (loadHandle.Status != AsyncOperationStatus.Succeeded)
            {
                GameLog.Error($"Failed to load asset: {label}");
            }

            var dictionary = new Dictionary<string, int>();

            for (var i = 0; i < loadHandle.Result.Count; i++)
            {
                var location = locations[i];
                var address = location.PrimaryKey;

                if (dictionary.ContainsKey(address))
                {
                    continue;
                }
                
                dictionary.Add(address, i);
            }

            return new ResourceCache
            {
                Label = label, 
                cachedObjects = loadHandle,
                orderDictionary = dictionary
            };
        }

        private static async UniTask ReleaseResourcesByLabelAsyncInternal(string label)
        {
            ValidateInitialize();

            var locationHandle = Addressables.LoadResourceLocationsAsync(label);
            await locationHandle.Task;

            if (locationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                GameLog.Error($"Failed to load label: {label}");
            }
            
            Addressables.Release(locationHandle);
        }
        
        // private static void ReleaseInternal(AsyncOperationHandle handle)
        // {
        //     ValidateInitialize();
        //     handle.Release();
        // }

        private static async UniTask<SceneInstance> LoadSceneAsyncInternal(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
        {
            ValidateInitialize();

            var scene = Addressables.LoadSceneAsync(sceneName, mode, activateOnLoad, priority);
            await scene.Task;

            if (scene.Status != AsyncOperationStatus.Succeeded)
            {
                GameLog.Error($"Failed to load scene: {sceneName}");
                return new SceneInstance();                
            }
            
            return scene.Result;
        }

        private static async UniTask UnloadSceneAsyncInternal(SceneInstance scene, bool unloadAllLoadedObjects = true)
        {
            ValidateInitialize();

            var result = Addressables.UnloadSceneAsync(scene, true);
            await result.Task;

            if (result.Status != AsyncOperationStatus.Succeeded)
            {
                GameLog.Error($"Failed to unload scene: {scene.Scene.name}");
                return;
            }

            await Resources.UnloadUnusedAssets();
        }

        internal static string GetAssetReferenceKey(AssetReference asset)
        {
            ValidateInitialize();
            return asset.RuntimeKey.ToString();
        }
    }
}
#endif