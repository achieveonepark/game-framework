#if USE_QUICK_SAVE
using Achieve.QuickSave;

namespace GameFramework.Data.Player
{
    /// <summary>
    /// PlayerManager의 데이터를 빠르게 저장/불러오기하는 내부 클래스입니다.
    /// USE_QUICK_SAVE 심볼이 정의된 경우에만 컴파일됩니다.
    /// Achieve.QuickSave 라이브러리를 사용하며, 암호화와 버전 관리를 지원합니다.
    /// internal 접근 제한자이므로 GameFramework.Data.Player 네임스페이스 내부에서만 사용됩니다.
    /// </summary>
    internal class QuickSave
    {
        // QuickSave 라이브러리를 빌더 패턴으로 설정합니다
        // UseEncryption: 저장 파일을 암호화하여 사용자가 직접 수정하지 못하게 합니다
        // UseVersion: 저장 파일의 버전을 관리합니다 (나중에 데이터 구조가 바뀌었을 때 마이그레이션에 사용)
        private readonly QuickSave<Player> _quickSave = new QuickSave<Player>.Builder()
            .UseEncryption("348GJ32ndh@R*gh#")
            .UseVersion(0)
            .Build();

        /// <summary>
        /// Player 데이터를 파일에 저장합니다.
        /// MemoryPack으로 직렬화된 후 암호화되어 디스크에 기록됩니다.
        /// </summary>
        /// <param name="player">저장할 PlayerManager 인스턴스 (Player 타입으로 사용됨)</param>
        internal void Save(Player player)
        {
            _quickSave.SaveData(player);
        }

        /// <summary>
        /// 저장된 파일에서 Player 데이터를 불러옵니다.
        /// 복호화 후 MemoryPack으로 역직렬화하여 반환합니다.
        /// </summary>
        /// <returns>불러온 Player 인스턴스</returns>
        internal Player Load()
        {
            return _quickSave.LoadData();
        }
    }
}
#endif
