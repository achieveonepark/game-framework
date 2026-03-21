namespace GameFramework
{
    /// <summary>
    /// 싱글톤(Singleton) 패턴을 구현하는 클래스가 따라야 하는 인터페이스입니다.
    /// 싱글톤이란 프로그램 전체에서 인스턴스가 하나만 존재하도록 보장하는 디자인 패턴입니다.
    /// 이 인터페이스를 구현하면 싱글톤 초기화 및 정리 로직을 표준화할 수 있습니다.
    /// </summary>
    public interface ISingleton
    {
        /// <summary>
        /// 싱글톤 인스턴스를 초기화합니다.
        /// 처음 인스턴스가 생성될 때 자동으로 호출됩니다.
        /// </summary>
        public void InitializeSingleton();

        /// <summary>
        /// 싱글톤 인스턴스를 정리하고 리소스를 해제합니다.
        /// 인스턴스가 파괴될 때 호출됩니다.
        /// </summary>
        public void ClearSingleton();
    }
}
