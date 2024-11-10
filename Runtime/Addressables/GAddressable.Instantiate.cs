#if USE_ADDRESSABLE
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
#if USE_TMP
using TMPro;
#endif
namespace GameFramework.Addressable
{
    public static partial class GAddressable
    {
        private static async UniTask<T> InstantiateAsyncInternal<T>(string address) where T : Object
        {
            ValidateInitialize();
            return AfterProcessInstantiate<T>(await Addressables.InstantiateAsync(address));
        }
        
        private static async UniTask<T> InstantiateAsyncInternal<T>(string address, Vector3 position, Quaternion rotation) where T : Object
        {
            ValidateInitialize();

            var obj = await Addressables.InstantiateAsync(address);
            
            if (obj == null)
            {
                return default;
            }
            
            if (Application.isEditor)
            {
                FindShader(obj);
            }

            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }
            
            if (obj.TryGetComponent<T>(out var instance))
            {
                return instance;    
            }

            return null;
        }
        
        
        
        private static async UniTask<T> InstantiateAsyncInternal<T>(string address, Transform parent, Vector3 position, Quaternion rotation) where T : Object
        {
            ValidateInitialize();

            var obj = await Addressables.InstantiateAsync(address, position, rotation, parent);
            obj.transform.localPosition = position;
            
            if (obj == null)
            {
                return default;
            }

            if (Application.isEditor)
            {
                FindShader(obj);
            }
            
            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            if (obj.TryGetComponent<T>(out var instance))
            {
                return instance;    
            }

            return null;
        }

        private static async UniTask<T> InstantiateAsyncInternal<T>(AssetReference assetRef) where T : Object
        {
            ValidateInitialize();

            var obj = await Addressables.InstantiateAsync(assetRef);
            
            if (obj == null)
            {
                return default;
            }

            if (Application.isEditor)
            {
                FindShader(obj);
            }
            
            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            if (obj.TryGetComponent<T>(out var instance))
            {
                return instance;    
            }

            return null;
        }

        private static async UniTask<T> InstantiateAsyncInternal<T>(AssetReference assetRef, Vector3 position, Quaternion rotation) where T : Object
        {
            ValidateInitialize();

            var obj = await Addressables.InstantiateAsync(assetRef, position, rotation);
            
            if (obj == null)
            {
                return default;
            }

            if (Application.isEditor)
            {
                FindShader(obj);
            }
            
            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            if (obj.TryGetComponent<T>(out var instance))
            {
                return instance;    
            }

            return null;
        }

        private static async UniTask<T> InstantiateAsyncInternal<T>(AssetReference assetRef, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            ValidateInitialize();
            return AfterProcessInstantiate<T>(await Addressables.InstantiateAsync(assetRef, position, rotation, parent));
        }

        private static void ReleaseInternal(GameObject obj, bool isDestroy = false)
        {
            Addressables.ReleaseInstance(obj);

            if (isDestroy)
            {
                Object.Destroy(obj);
            }
        }

        private static T AfterProcessInstantiate<T>(GameObject obj) where T : Object
        {
            if (obj == null)
            {
                return default;
            }

            if (Application.isEditor)
            {
                FindShader(obj);
            }
            
            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            if (obj.TryGetComponent<T>(out var instance) is false)
            {
                GameLog.Error($"<color=yellow>{typeof(T)}</color> Component는 <color=yellow>{obj.name.Replace("(Clone)", "")}</color>에 존재하지 않습니다. 등록을 했음에도 이 에러가 뜬다면, Addressable 빌드를 진행해주세요.");
                return default;
            }

            return instance;    
        }

        internal static void FindShader(GameObject obj)
        {
            #if ENABLE_TMP
            var txts = obj.GetComponentsInChildren<TMP_Text>(true);
            
            foreach (var txt in txts)
            {
                var shaderName = txt.fontSharedMaterial.shader.name;
                var shader = Shader.Find(shaderName);
                txt.fontSharedMaterial.shader = shader;
            }
            #endif
            
            var renderers = obj.GetComponentsInChildren<Renderer>(true);
            
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    var shader = Shader.Find(material.shader.name);  // Shader 찾기
                    if (shader != null)
                    {
                        material.shader = shader;  // Shader를 재할당
                    }
                    else
                    {
                        Debug.LogError($"Shader {material.shader.name} not found.");
                    }
                }
            }
        }
    }
}
#endif