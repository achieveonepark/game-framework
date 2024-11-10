using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// IL2CPP 빌드에서 인크리멘탈 빌드를 활성화할지 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetIncrementalIl2CppBuildCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetIncrementalIl2CppBuildCommand);

        private readonly BuildTargetGroup _targetGroup;
        private readonly bool _enabled;
        private bool _oldEnabled;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetIncrementalIl2CppBuildCommand
        (
            BuildTargetGroup targetGroup,
            bool enabled
        )
        {
            _targetGroup = targetGroup;
            _enabled = enabled;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetIncrementalIl2CppBuildCommand
        (
            BuildTarget target,
            bool enabled
        ) : this(BuildPipeline.GetBuildTargetGroup(target), enabled)
        {
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldEnabled = PlayerSettings.GetIncrementalIl2CppBuild(_targetGroup);
            PlayerSettings.SetIncrementalIl2CppBuild(_targetGroup, _enabled);
            return Success(_enabled.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetIncrementalIl2CppBuild(_targetGroup, _oldEnabled);
        }
    }
}