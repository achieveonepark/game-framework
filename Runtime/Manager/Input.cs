using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    /// <summary>
    /// 게임의 입력(Input)을 관리하는 매니저입니다.
    /// 키보드, 터치, 게임패드 등 다양한 입력 장치를 통합 관리하기 위한 클래스입니다.
    /// 현재는 초기 구조만 갖추고 있으며, 게임에 맞는 입력 처리 로직을 추가할 수 있습니다.
    /// </summary>
    public class InputManager : IManager
    {
        /// <summary>
        /// InputManager를 초기화합니다.
        /// 현재는 별도의 초기화 로직이 없습니다.
        /// </summary>
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}
