using System;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    /// <summary>
    /// 게임 전체에서 사용하는 정적 로그 유틸리티 클래스입니다.
    /// GameLog.Debug("메시지") 처럼 간편하게 로그를 출력할 수 있습니다.
    /// 내부적으로 IGameLog 구현체(GLog)를 사용하며, 처음 호출 시 자동으로 생성됩니다.
    /// </summary>
    public static class GameLog
    {
        // 실제 로그 출력을 담당하는 구현체. 처음 사용 시 자동으로 초기화됩니다.
        private static IGameLog _log;

        /// <summary>현재 로그 레벨을 변경합니다. 이 레벨보다 낮은 중요도의 로그는 출력되지 않습니다.</summary>
        public static void SetLogLevel(LogLevel level) => _log.LogLevel = level;

        /// <summary>개발 시 세부 정보 확인용 디버그 로그를 출력합니다.</summary>
        public static void Debug(string message) => LogBase(LogLevel.Debug, message);

        /// <summary>일반적인 정보 메시지를 출력합니다.</summary>
        public static void Info(string message) => LogBase(LogLevel.Info, message);

        /// <summary>주의가 필요한 경고 메시지를 출력합니다.</summary>
        public static void Warning(string message) => LogBase(LogLevel.Warning, message);

        /// <summary>오류 메시지를 출력합니다.</summary>
        public static void Error(string message) => LogBase(LogLevel.Error, message);

        /// <summary>
        /// 치명적 오류를 기록하고 예외를 던집니다.
        /// 게임을 계속 실행할 수 없는 심각한 상황에서 사용합니다.
        /// throw GameLog.Fatal(new Exception("...")) 형태로 사용합니다.
        /// </summary>
        /// <param name="exception">발생한 예외 객체</param>
        /// <returns>실제로는 항상 예외를 던지므로 반환되지 않습니다.</returns>
        public static Exception Fatal(Exception exception) => throw LogBase(LogLevel.Fatal, exception.Message);

        /// <summary>
        /// 내부 로그 출력 메서드입니다. 외부에서 직접 사용하는 것보다 Debug/Info/Warning/Error/Fatal 메서드를 사용하세요.
        /// _log가 null이면 자동으로 GLog를 생성하여 초기화합니다.
        /// </summary>
        /// <param name="level">로그 중요도 레벨</param>
        /// <param name="message">출력할 메시지</param>
        /// <returns>Fatal 레벨이면 Exception을 반환하고 예외를 던집니다. 나머지는 null 반환.</returns>
        public static Exception LogBase(LogLevel level, string message)
        {
            // _log가 null이면 기본 GLog 구현체를 생성합니다
            if (_log == null)
            {
                _log = new GLog();
            }

            // 현재 설정된 로그 레벨보다 낮은 중요도의 메시지는 출력하지 않습니다
            if(_log.LogLevel < level)
            {
                return null;
            }

            // Fatal 레벨은 로그 출력 후 예외를 던져 실행을 중단합니다
            if (level == LogLevel.Fatal)
            {
                _log.Log(level, message);
                throw new Exception(message);
            }

            _log.Log(level, message);
            return level == LogLevel.Fatal ? new Exception(message) : null;
        }
    }
}
