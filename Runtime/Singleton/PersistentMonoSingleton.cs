using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 씬이 변경되어도 파괴되지 않고 유지되는 MonoBehaviour 싱글톤입니다.
    /// DontDestroyOnLoad를 사용하여 게임 전체 생명주기 동안 인스턴스를 유지합니다.
    /// PopupManager, SoundManager 처럼 게임 전반에 걸쳐 항상 존재해야 하는 오브젝트에 사용합니다.
    /// <see cref="MonoSingleton{T}"/>와 달리 씬 전환 후에도 파괴되지 않습니다.
    /// </summary>
    /// <typeparam name="T">영구적인 싱글톤으로 만들 MonoBehaviour 자식 클래스 타입</typeparam>
    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        /// <summary>
        /// 초기화 완료 시 호출됩니다.
        /// DontDestroyOnLoad를 호출하여 씬이 전환되어도 이 오브젝트가 파괴되지 않도록 합니다.
        /// 에디터 모드에서는 동작하지 않고 플레이 모드에서만 적용됩니다.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            // 게임이 실행 중일 때만 DontDestroyOnLoad를 적용합니다
            // (에디터에서 플레이하지 않을 때는 씬 전환이 없으므로 불필요)
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
