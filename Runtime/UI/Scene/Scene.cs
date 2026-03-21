using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// IScene 인터페이스를 구현하는 MonoBehaviour 기반 추상 씬 클래스입니다.
    /// 각 씬의 루트 오브젝트에 이 클래스를 상속한 컴포넌트를 붙이면
    /// SceneManager가 씬 전환 시 OnSceneStart/OnSceneEnd를 자동으로 호출합니다.
    /// abstract 클래스이므로 직접 사용하지 않고 반드시 상속하여 구현해야 합니다.
    /// </summary>
    public abstract class Scene : MonoBehaviour, IScene
    {
        /// <summary>
        /// 씬이 완전히 로드된 후 SceneManager에 의해 호출됩니다.
        /// 자식 클래스에서 구현하여 씬 진입 시 필요한 초기화 로직을 작성합니다.
        /// </summary>
        public abstract UniTask OnSceneStart();

        /// <summary>
        /// 씬이 언로드되기 전에 SceneManager에 의해 호출됩니다.
        /// 자식 클래스에서 구현하여 씬 종료 시 정리 로직을 작성합니다.
        /// </summary>
        public abstract UniTask OnSceneEnd();
    }
}
