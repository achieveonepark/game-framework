using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 정리할 때 AssetDatabase.SaveAssets를 실행하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SaveAssetsOnCleanUpCommand : BuildCommandBase
    {
        public override string Tag => nameof(SaveAssetsOnCleanUpCommand);

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            return Success();
        }

        /// <summary>
        /// 정리할 때 호출됩니다
        /// </summary>
        protected override void DoCleanUp()
        {
            AssetDatabase.SaveAssets();
        }
    }
}