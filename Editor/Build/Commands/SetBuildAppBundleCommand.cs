using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// apk 대신 aab로 빌드할지 여부를 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetBuildAppBundleCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetBuildAppBundleCommand);
        private readonly bool _buildAppBundle;
        private bool _oldBuildAppBundle;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetBuildAppBundleCommand(bool buildAppBundle)
        {
            _buildAppBundle = buildAppBundle;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldBuildAppBundle = EditorUserBuildSettings.buildAppBundle;
            EditorUserBuildSettings.buildAppBundle = _buildAppBundle;
            return Success(_buildAppBundle.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            EditorUserBuildSettings.buildAppBundle = _oldBuildAppBundle;
        }
    }
}