namespace GameFramework
{
    /// <summary>
    /// 게임 로그 시스템의 인터페이스입니다.
    /// 이 인터페이스를 구현하면 다양한 로그 출력 방식(Unity 콘솔, 파일, 서버 등)을 교체해서 사용할 수 있습니다.
    /// </summary>
    public interface IGameLog
    {
        /// <summary>
        /// 현재 활성화된 로그 레벨입니다.
        /// 설정된 레벨에 따라 어떤 로그를 표시할지 필터링할 수 있습니다.
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// 지정한 레벨로 메시지를 로그에 기록합니다.
        /// </summary>
        /// <param name="level">로그의 중요도 수준</param>
        /// <param name="message">기록할 메시지</param>
        void Log(LogLevel level, object message);
    }
}
