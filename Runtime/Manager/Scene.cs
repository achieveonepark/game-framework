using System;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace GameFramework
{
    public class SceneManager : IManager
    {
        public IScene Current { get; private set; }
        public string CurrentSceneName => UnitySceneManager.GetActiveScene().name;
        private bool _isLoading;

        public event Action OnSceneLoadStarted;
        public static event Action OnSceneLoadCompleted;

        public UniTask Initialize()
        {
            // Any specific startup logic for SceneManager can go here.
            return UniTask.CompletedTask;
        }

        public async UniTask LoadSceneAsync(string sceneName)
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
            await UnitySceneManager.LoadSceneAsync(sceneName).ToUniTask();

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

            // Important: Events should be cleared if they are meant to be transient per load
            // OnSceneLoadStarted = null; 
            // OnSceneLoadCompleted = null; 

            await Current.OnSceneStart();
        }

        public async UniTask ReloadSceneAsync()
        {
            if (Current == null)
            {
                throw new NullReferenceException($"Not found {nameof(Current)} in SceneManager.");
            }

            string currentSceneName = CurrentSceneName;
            await LoadSceneAsync(currentSceneName);
        }

        public async UniTask UnloadSceneAsync(string sceneName)
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