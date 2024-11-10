using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Android의 targetArchitectures를 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetAndroidTargetArchitecturesCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetAndroidTargetArchitecturesCommand);
        private readonly AndroidArchitecture _targetArchitectures;
        private AndroidArchitecture _oldTargetArchitectures;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetAndroidTargetArchitecturesCommand
        (
            AndroidArchitecture targetArchitectures
        )
        {
            _targetArchitectures = targetArchitectures;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldTargetArchitectures = PlayerSettings.Android.targetArchitectures;
            PlayerSettings.Android.targetArchitectures = _targetArchitectures;
            return Success(_targetArchitectures.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.Android.targetArchitectures = _oldTargetArchitectures;
        }
    }
}