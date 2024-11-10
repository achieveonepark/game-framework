using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Mono2x 빌드 또는 IL2CPP 빌드를 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetScriptingBackendCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetScriptingBackendCommand);

        private readonly BuildTargetGroup _targetGroup;
        private readonly ScriptingImplementation _backend;
        private ScriptingImplementation _oldBackend;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingBackendCommand
        (
            BuildTargetGroup targetGroup,
            ScriptingImplementation backend
        )
        {
            _targetGroup = targetGroup;
            _backend = backend;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingBackendCommand
        (
            BuildTarget target,
            ScriptingImplementation backend
        ) : this(BuildPipeline.GetBuildTargetGroup(target), backend)
        {
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldBackend = PlayerSettings.GetScriptingBackend(_targetGroup);
            PlayerSettings.SetScriptingBackend(_targetGroup, _backend);
            return Success(_backend.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetScriptingBackend(_targetGroup, _oldBackend);
        }
    }
}