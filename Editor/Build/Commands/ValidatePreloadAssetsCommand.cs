using System.Linq;
using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Preload Assets가 정상인지 검증하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class ValidatePreloadAssetsCommand : BuildCommandBase
    {
        public override string Tag => nameof(ValidatePreloadAssetsCommand);

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var hasMissing = PlayerSettings
                .GetPreloadedAssets()
                .Any(x => x == null);

            return hasMissing ? Error() : Success();
        }
    }
}