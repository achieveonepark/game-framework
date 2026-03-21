using System;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace GameFramework
{
    /// <summary>
    /// 씬(Scene) 전환과 로딩을 관리하는 매니저입니다.
    /// Unity 기본 SceneManager를 감싸서 게임 프레임워크에 맞는 씬 전환 흐름을 제공합니다.
    /// 씬 시작/종료 이벤트를 지원하며, 동시에 여러 씬을 로드하는 것을 방지합니다.
    /// </summary>
    public class SceneManager : IManager
    {
        /// <summary>현재 활성화된 씬의 IScene 컴포넌트입니다. 씬 오브젝트에 붙어 있어야 합니다.</summary>
        public IScene Current { get; private set; }

        /// <summary>현재 Unity에서 활성화된 씬의 이름을 반환합니다.</summary>
        public string CurrentSceneName => UnitySceneManager.GetActiveScene().name;

        // 씬 전환 중인지 여부를 나타내는 플래그. 중복 로딩을 방지합니다.
        private bool _isLoading;

        /// <summary>씬 로딩이 시작될 때 발생하는 이벤트입니다. 로딩 화면 표시 등에 활용합니다.</summary>
        public event Action OnSceneLoadStarted;

        /// <summary>씬 로딩이 완료되었을 때 발생하는 이벤트입니다. 정적(static)이므로 어디서든 구독 가능합니다.</summary>
        public static event Action OnSceneLoadCompleted;

        /// <summary>
        /// SceneManager를 초기화합니다.
        /// </summary>
        public UniTask Initialize()
        {
            // Any specific startup logic for SceneManager can go here.
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 지정한 이름의 씬을 비동기로 로드합니다.
        /// 현재 씬의 OnSceneEnd를 먼저 호출한 후, 새 씬을 로드하고 OnSceneStart를 호출합니다.
        /// 이미 로딩 중이면 아무것도 하지 않습니다.
        /// </summary>
        /// <param name="sceneName">로드할 씬 이름 (Build Settings에 등록된 씬 이름)</param>
        public async UniTask LoadSceneAsync(string sceneName)
        {
            // 이미 씬 전환 중이면 중복 로딩을 방지합니다
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;

            // 현재 씬이 있으면 씬 종료 처리를 먼저 수행합니다
            if (Current != null)
            {
                await Current.OnSceneEnd();
            }

            // 씬 로딩 시작 이벤트 발생 (로딩 UI 표시 등에 활용)
            OnSceneLoadStarted?.Invoke();
            await UnitySceneManager.LoadSceneAsync(sceneName).ToUniTask();

            // 씬 로딩 완료 이벤트 발생
            OnSceneLoadCompleted?.Invoke();
            var scene = UnitySceneManager.GetActiveScene();
            var roots = scene.GetRootGameObjects();

            // 새로 로드된 씬의 루트 오브젝트에서 IScene 컴포넌트를 찾습니다
            foreach (var root in roots)
            {
                if (root.TryGetComponent<IScene>(out var sceneComponent))
                {
                    Current = sceneComponent;
                    break;
                }
            }

            // IScene 컴포넌트를 가진 오브젝트가 없으면 씬 설정이 잘못된 것입니다
            if (Current == null)
            {
                throw new NullReferenceException($"Not found {nameof(scene)} in SceneManager.");
            }

            _isLoading = false;

            // Important: Events should be cleared if they are meant to be transient per load
            // OnSceneLoadStarted = null;
            // OnSceneLoadCompleted = null;

            // 새 씬의 시작 처리를 수행합니다
            await Current.OnSceneStart();
        }

        /// <summary>
        /// 현재 씬을 다시 로드합니다. 씬 리셋 또는 재시작에 사용합니다.
        /// Current가 null이면 예외를 던집니다.
        /// </summary>
        public async UniTask ReloadSceneAsync()
        {
            if (Current == null)
            {
                throw new NullReferenceException($"Not found {nameof(Current)} in SceneManager.");
            }

            // 현재 씬 이름을 저장해 두고 다시 로드합니다
            string currentSceneName = CurrentSceneName;
            await LoadSceneAsync(currentSceneName);
        }

        /// <summary>
        /// 지정한 씬을 언로드(Unload)합니다.
        /// 멀티 씬 구조에서 특정 씬만 제거할 때 사용합니다.
        /// 로딩 중이거나 Current가 null이면 아무것도 하지 않습니다.
        /// </summary>
        /// <param name="sceneName">언로드할 씬 이름</param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (_isLoading || Current == null)
            {
                return;
            }

            _isLoading = true;
            // 씬 종료 처리 후 언로드합니다
            await Current.OnSceneEnd();

            await UnitySceneManager.UnloadSceneAsync(sceneName);
            Current = null;
            _isLoading = false;
        }
    }
}
