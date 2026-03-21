using System;
using GameFramework;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// IGameLog 인터페이스를 구현하는 실제 로그 출력 클래스입니다.
    /// 로그 레벨에 따라 색상을 달리하여 Unity 콘솔에 출력합니다.
    /// internal 접근 제한자이므로 GameFramework 어셈블리 내부에서만 사용됩니다.
    /// </summary>
    internal class GLog : IGameLog
    {
        /// <summary>
        /// 현재 활성화된 로그 레벨입니다.
        /// 이 레벨보다 낮은 중요도의 로그는 출력되지 않도록 필터링할 수 있습니다.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 지정한 로그 레벨에 맞는 형식으로 메시지를 Unity 콘솔에 출력합니다.
        /// Fatal 레벨은 예외(Exception)를 던져 게임 실행을 중단시킵니다.
        /// </summary>
        /// <param name="level">로그의 중요도 수준 (Info, Debug, Warning, Error, Fatal)</param>
        /// <param name="message">출력할 메시지 내용</param>
        public void Log(LogLevel level, object message)
        {
            // 로그 레벨에 따라 콘솔에 표시될 색상을 결정합니다
            string color = level switch
            {
                LogLevel.Info => "green",
                LogLevel.Debug => "white",
                LogLevel.Warning => "yellow",
                LogLevel.Error => "red",
                LogLevel.Fatal => "red",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };

            // 로그 레벨에 따라 Unity의 적절한 로그 메서드를 선택합니다
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    // 일반 정보 및 디버그 메시지는 Debug.Log로 출력
                    Debug.Log($"<color={color}>[{level}] {message}</color>");
                    break;
                case LogLevel.Warning:
                    // 경고 메시지는 노란색으로 Debug.LogWarning 사용
                    Debug.LogWarning($"<color={color}>[{level}] {message}</color>");
                    break;
                case LogLevel.Error:
                    // 오류 메시지는 빨간색으로 Debug.LogError 사용
                    Debug.LogError($"<color={color}>[{level}] {message}</color>");
                    break;
                case LogLevel.Fatal:
                    // 치명적 오류는 예외를 던져 즉시 실행을 중단시킵니다
                    throw new Exception($"<color={color}>[{level}] {message}</color>");
            }
        }
    }
}
