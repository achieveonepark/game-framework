using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Switch Platform을 실행하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SwitchActiveBuildTargetCommand : BuildCommandBase
    {
        public override string Tag => nameof(SwitchActiveBuildTargetCommand);

        private readonly BuildTargetGroup _targetGroup;
        private readonly BuildTarget _target;

        /// <summary>
        /// 생성자
        /// </summary>
        public SwitchActiveBuildTargetCommand
        (
            BuildTargetGroup targetGroup,
            BuildTarget target
        )
        {
            _targetGroup = targetGroup;
            _target = target;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SwitchActiveBuildTargetCommand
        (
            BuildTarget target
        ) : this(BuildPipeline.GetBuildTargetGroup(target), target)
        {
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var currentTarget = EditorUserBuildSettings.activeBuildTarget;

            if (currentTarget == _target) return Success(_target.ToString());

            if (EditorUserBuildSettings.SwitchActiveBuildTarget(_targetGroup, _target))
            {
                return Success(_target.ToString());
            }

            return Error(_target.ToString());
        }
    }
}