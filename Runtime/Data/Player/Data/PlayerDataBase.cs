namespace GameFramework
{
    /// <summary>
    /// 플레이어 개별 데이터 항목의 기본 클래스입니다.
    /// 인벤토리 아이템, 스킬, 퀘스트 등 고유 ID를 가진 플레이어 데이터 항목이 이 클래스를 상속합니다.
    /// </summary>
    public class PlayerDataBase
    {
        /// <summary>
        /// 데이터 항목의 고유 식별자입니다.
        /// 테이블(DB)에서 해당 데이터를 찾는 키로 사용됩니다.
        /// </summary>
        public int Id;
    }
}
