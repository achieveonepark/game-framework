using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 심볼을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetScriptingDefineSymbolsCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetScriptingDefineSymbolsCommand);

        private readonly BuildTargetGroup _targetGroup;
        private readonly string _defines;
        private readonly bool _isSkip;
        private string _oldSymbols;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingDefineSymbolsCommand
        (
            BuildTargetGroup targetGroup,
            string defines
        )
        {
            _targetGroup = targetGroup;
            _defines = defines;
            _isSkip = defines == null;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingDefineSymbolsCommand
        (
            BuildTarget target,
            string defines
        ) : this(BuildPipeline.GetBuildTargetGroup(target), defines)
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingDefineSymbolsCommand
        (
            BuildTargetGroup targetGroup,
            string[] defines
        ) : this(targetGroup, string.Join(";", defines))
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SetScriptingDefineSymbolsCommand
        (
            BuildTarget target,
            string[] defines
        ) : this(BuildPipeline.GetBuildTargetGroup(target), defines)
        {
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            if (_isSkip) return Success("Skip");

            _oldSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(_targetGroup);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(_targetGroup, _defines);

            var message = string.Join("\n", _defines.Split(';'));

            return Success($"\n\n{message}");
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            if (_isSkip) return;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(_targetGroup, _oldSymbols);
        }
    }
}