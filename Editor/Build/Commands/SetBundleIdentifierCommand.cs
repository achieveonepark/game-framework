using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Application Identifier를 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetApplicationIdentifierCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetApplicationIdentifierCommand);
        private readonly string _applicationIdentifier;
        private string _oldApplicationIdentifier;
        
        /// <summary>
        /// 생성자
        /// </summary>
        public SetApplicationIdentifierCommand(string applicationIdentifier)
        {
            _applicationIdentifier = applicationIdentifier;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldApplicationIdentifier = PlayerSettings.applicationIdentifier;
            PlayerSettings.applicationIdentifier = _applicationIdentifier;
            return Success(_applicationIdentifier);
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.applicationIdentifier = _oldApplicationIdentifier;
        }
    }
}