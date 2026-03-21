namespace GameFramework
{
    /// <summary>
    /// 플레이어 데이터 컨테이너가 구현해야 하는 인터페이스입니다.
    /// PlayerManager는 이 인터페이스를 통해 다양한 종류의 데이터 컨테이너를 타입에 무관하게 관리합니다.
    /// 컨테이너란 특정 종류의 플레이어 데이터(예: 인벤토리, 스킬 목록 등)를 모아 관리하는 클래스입니다.
    /// </summary>
    public interface IPlayerDataContainerBase
    {
        /// <summary>
        /// 이 컨테이너를 식별하는 고유 키입니다.
        /// PlayerManager에서 컨테이너를 찾을 때 이 키를 사용합니다.
        /// 보통 컨테이너 클래스의 이름(typeof(T).Name)을 사용합니다.
        /// </summary>
        string DataKey { get; }
    }
}
