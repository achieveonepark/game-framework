namespace GameFramework
{
    /// <summary>
    /// 로그 메시지의 중요도 수준을 나타내는 열거형입니다.
    /// 숫자가 낮을수록 더 심각한 수준을 의미합니다.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>치명적 오류. 게임을 계속 실행할 수 없는 상황입니다. 예외를 던져 즉시 중단시킵니다.</summary>
        Fatal = 0,

        /// <summary>오류. 기능이 제대로 동작하지 않는 상황입니다. 즉각적인 수정이 필요합니다.</summary>
        Error,

        /// <summary>경고. 잠재적인 문제가 있지만 게임은 계속 실행됩니다.</summary>
        Warning,

        /// <summary>정보. 게임의 주요 흐름을 추적하기 위한 일반 정보 메시지입니다.</summary>
        Info,

        /// <summary>디버그. 개발 중 세부 정보 확인을 위한 메시지입니다. 배포 시에는 비활성화하는 것이 좋습니다.</summary>
        Debug
    }
}
