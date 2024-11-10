using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// IL2CPP 빌드의 컴파일러 구성을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetIl2CppCompilerConfigurationCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetIl2CppCompilerConfigurationCommand);

        private readonly BuildTargetGroup _targetGroup;
        private readonly Il2CppCompilerConfiguration _configuration;
        private Il2CppCompilerConfiguration _oldConfiguration;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetIl2CppCompilerConfigurationCommand
        (
            BuildTargetGroup targetGroup,
            Il2CppCompilerConfiguration configuration
        )
        {
            _targetGroup = targetGroup;
            _configuration = configuration;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetIl2CppCompilerConfigurationCommand
        (
            BuildTarget target,
            Il2CppCompilerConfiguration configuration
        ) : this(BuildPipeline.GetBuildTargetGroup(target), configuration)
        {
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldConfiguration = PlayerSettings.GetIl2CppCompilerConfiguration(_targetGroup);
            PlayerSettings.SetIl2CppCompilerConfiguration(_targetGroup, _configuration);
            return Success(_configuration.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetIl2CppCompilerConfiguration(_targetGroup, _oldConfiguration);
        }
    }
}