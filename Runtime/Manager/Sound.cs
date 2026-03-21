using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    /// <summary>
    /// 게임의 사운드(Sound)를 관리하는 매니저입니다.
    /// BGM(배경음악), SFX(효과음) 등의 재생, 정지, 볼륨 조절 기능을 제공합니다.
    /// 현재는 기본 구조만 갖추고 있으며, 게임에 맞는 사운드 로직을 추가할 수 있습니다.
    /// </summary>
    public class SoundManager : IManager
    {
        /// <summary>
        /// SoundManager를 초기화합니다.
        /// 실제 사용 시에는 오디오 클립 로딩, AudioSource 초기화 등의 로직이 들어와야 합니다.
        /// </summary>
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}
