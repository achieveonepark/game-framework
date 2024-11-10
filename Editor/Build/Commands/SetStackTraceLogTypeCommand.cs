using UnityEditor;
using UnityEngine;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 스택 트레이스 옵션을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetStackTraceLogTypeCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetStackTraceLogTypeCommand);

        private readonly StackTraceLogType _type;
        private StackTraceLogType _oldType;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetStackTraceLogTypeCommand(StackTraceLogType type)
        {
            _type = type;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldType = PlayerSettings.GetStackTraceLogType(LogType.Log);

            SetStackTraceLogType(_type);

            return Success(_type.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            SetStackTraceLogType(_oldType);
        }

        /// <summary>
        /// 모든 로그의 스택 트레이스 옵션을 설정합니다
        /// </summary>
        private static void SetStackTraceLogType(StackTraceLogType type)
        {
            PlayerSettings.SetStackTraceLogType(LogType.Error, type);
            PlayerSettings.SetStackTraceLogType(LogType.Assert, type);
            PlayerSettings.SetStackTraceLogType(LogType.Warning, type);
            PlayerSettings.SetStackTraceLogType(LogType.Log, type);
            PlayerSettings.SetStackTraceLogType(LogType.Exception, type);
        }
    }
}