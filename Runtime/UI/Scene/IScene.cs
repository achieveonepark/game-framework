using Cysharp.Threading.Tasks;

namespace GameFramework
{
    /// <summary>
    /// 게임 씬의 시작/종료 생명주기(Lifecycle)를 정의하는 인터페이스입니다.
    /// SceneManager는 씬을 로드할 때 이 인터페이스를 구현한 컴포넌트를 찾아
    /// OnSceneStart와 OnSceneEnd를 자동으로 호출합니다.
    /// 각 씬의 루트 오브젝트에 이 인터페이스를 구현하는 컴포넌트를 붙여야 합니다.
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// 씬이 완전히 로드된 후 호출됩니다.
        /// 씬 진입 시 필요한 초기화 작업(데이터 로딩, UI 설정 등)을 여기서 수행합니다.
        /// 비동기 작업을 await으로 기다릴 수 있습니다.
        /// </summary>
        UniTask OnSceneStart();

        /// <summary>
        /// 씬이 언로드되기 전에 호출됩니다.
        /// 씬 종료 시 필요한 정리 작업(이벤트 해제, 저장 등)을 여기서 수행합니다.
        /// </summary>
        UniTask OnSceneEnd();
    }
}
