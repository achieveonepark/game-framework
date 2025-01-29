using System;
using Cysharp.Threading.Tasks;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
    
namespace GameFramework
{
    public static class SceneManager
    {
        public static IScene Current { get; private set; }
        public static string CurrentSceneName => UnitySceneManager.GetActiveScene().name;
        private static bool _isLoading;

        public static event Action OnSceneLoadStarted;
        public static event Action OnSceneLoadCompleted;
        
        public static async UniTask LoadScene(string sceneName)
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;

            if (Current != null)
            {
                await Current.OnSceneEnd();
            }

            OnSceneLoadStarted?.Invoke();
            await UnitySceneManager.LoadSceneAsync(sceneName);
            OnSceneLoadCompleted?.Invoke();
            var scene = UnitySceneManager.GetActiveScene();
            var roots = scene.GetRootGameObjects();
            
            foreach (var root in roots)
            {
                if (root.TryGetComponent<IScene>(out var sceneComponent))
                {
                    Current = sceneComponent;
                    break;
                }
            }

            if (Current == null)
            {
                throw new NullReferenceException($"Not found {nameof(scene)} in SceneManager.");
            }

            _isLoading = false;

            OnSceneLoadStarted = null;
            OnSceneLoadCompleted = null;
            
            await Current.OnSceneStart();
        }

        public static async UniTask ReloadScene()
        {
            if (Current == null)
            {
                throw new NullReferenceException($"Not found {nameof(Current)} in SceneManager.");
            }
            
            string currentSceneName = CurrentSceneName;
            await LoadScene(currentSceneName);
        }
        
        public static async UniTask UnloadScene(string sceneName)
        {
            if (_isLoading || Current == null)
            {
                return;
            }
            
            _isLoading = true;
            await Current.OnSceneEnd();

            await UnitySceneManager.UnloadSceneAsync(sceneName);
            Current = null;
            _isLoading = false;
        }
    }
}