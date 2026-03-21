using Cysharp.Threading.Tasks;

namespace GameFramework.Manager
{
    /// <summary>
    /// 모든 매니저 클래스가 구현해야 하는 인터페이스입니다.
    /// 이 인터페이스를 구현하면 Core.Register()를 통해 게임 시스템에 등록할 수 있습니다.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// 매니저를 초기화합니다.
        /// Core에 등록될 때 자동으로 호출됩니다.
        /// UniTask를 반환하므로 비동기 초기화(예: 파일 로드, 네트워크 요청)가 가능합니다.
        /// </summary>
        UniTask Initialize();
    }
}
