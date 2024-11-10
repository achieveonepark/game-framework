#if USE_ADDRESSABLE
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GameFramework.Addressable
{
    public static partial class GAddressable
    {
        /// <summary>
        /// Loads the default Addressable resources.
        /// </summary>
        /// <returns></returns>
        public static async UniTask<IResourceLocator> InitializeAsync() 
            => await InitializeAsyncInternal();
        
        /// <summary>
        /// Loads Addressable assets by referencing the catalog.json file.
        /// </summary>
        /// <param name="catalog">Full path to catalog.json file stored in local cache or on a remote server.</param>
        /// <returns></returns>
        public static async UniTask<IResourceLocator> InitializeAsync(string catalog) 
            => await InitializeAsyncInternal(catalog);
        
        #region Instantiate
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it immediately.
        /// </summary>
        /// <param name="address">Addressable asset address.</param>
        /// <param name="position">Position for instantiation.</param>
        /// <param name="rotation">Rotation for instantiation.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> InstantiateAsync<T>(string address) where T : Object 
            => await InstantiateAsyncInternal<T>(address);
        
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it immediately at the specified position and rotation.
        /// </summary>
        /// <param name="address">Addressable asset address.</param>
        /// <param name="position">Position for instantiation.</param>
        /// <param name="rotation">Rotation for instantiation.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> InstantiateAsync<T>(string address, Vector3 position, Quaternion rotation) where T : Object 
            => await InstantiateAsyncInternal<T>(address, position, rotation);
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it with a parent transform, position, and rotation.
        /// </summary>
        /// <param name="address">Addressable asset address.</param>
        /// <param name="position">Position for instantiation.</param>
        /// <param name="rotation">Rotation for instantiation.</param>
        /// <param name="parent">Parent transform.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> InstantiateAsync<T>(string address, Transform parent, Vector3 position = default, Quaternion rotation = default) where T : Object 
            => await InstantiateAsyncInternal<T>(address, parent, position, rotation);
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it immediately using an AssetReference.
        /// </summary>
        /// <param name="assetRef">Asset reference.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> Instantiate<T>(AssetReference assetRef) where T : Object 
            => await InstantiateAsyncInternal<T>(assetRef);
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it at the specified position and rotation using an AssetReference.
        /// </summary>
        /// <param name="assetRef">Asset reference.</param>
        /// <param name="position">Position for instantiation.</param>
        /// <param name="rotation">Rotation for instantiation.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> InstantiateAsync<T>(AssetReference assetRef, Vector3 position, Quaternion rotation) where T : Object 
            => await InstantiateAsyncInternal<T>(assetRef, position, rotation);
        
        /// <summary>
        /// Loads the Addressable asset into memory cache and instantiates it with specified position, rotation, and parent transform using an AssetReference.
        /// </summary>
        /// <param name="assetRef">Asset reference.</param>
        /// <param name="position">Position for instantiation.</param>
        /// <param name="rotation">Rotation for instantiation.</param>
        /// <param name="parent">Parent transform.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> InstantiateAsync<T>(AssetReference assetRef, Vector3 position, Quaternion rotation, Transform parent) where T : Object 
            => await InstantiateAsyncInternal<T>(assetRef, position, rotation, parent);
        
        #endregion

        /// <summary>
        /// Caches resources labeled in the Inspector. Call GetObject after this process.
        /// </summary>
        /// <param name="label"></param>
        public static async UniTask<ResourceCache> LoadResourcesAsync(string label)
            => await LoadResourcesAsyncInternal(label);
        
        /// <summary>
        /// Releases all cached resources with the specified label from memory.
        /// </summary>
        public static async UniTask ReleaseResourcesByLabelAsync(string label)
            => await ReleaseResourcesByLabelAsyncInternal(label);

        /// <summary>
        /// Releases a cached resource from memory based on its address when GetObject was called.
        /// </summary>
        /// <param name="address"></param>
        public static void Release(GameObject obj, bool isDestroy = false) 
            => ReleaseInternal(obj, isDestroy);

        /// <summary>
        /// Loads a scene. Downloads it if no local cache is available.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="mode"></param>
        /// <param name="activateOnLoad"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static async UniTask<SceneInstance> LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
            => await LoadSceneAsyncInternal(sceneName, mode, activateOnLoad, priority);
        
        /// <summary>
        /// Unloads a scene with an option to release all loaded objects.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unloadAllLoadedObjects"></param>
        public static async UniTask UnloadSceneAsync(SceneInstance scene, bool unloadAllLoadedObjects = true)
            => await UnloadSceneAsyncInternal(scene, unloadAllLoadedObjects);
        
        /// <summary>
        /// Updates the resource catalog and downloads updated data to the local cache.
        /// </summary>
        /// <param name="label"></param>
        public static async UniTask UpdateResourceAsync(string label) 
            => await UpdateResourceAsyncInternal(label);

        /// <summary>
        /// Returns locator information for all resources registered in Addressables.
        /// </summary>
        public static IEnumerable<IResourceLocator> ResourceLocators 
            => Addressables.ResourceLocators;
    }
}
#endif