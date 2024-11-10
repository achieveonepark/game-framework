using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// WebGL 템플릿을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetWebGLTemplateCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetWebGLTemplateCommand);

        private readonly string _template;
        private string _oldTemplate;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetWebGLTemplateCommand(string template)
        {
            _template = template;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldTemplate = PlayerSettings.WebGL.template;
            PlayerSettings.WebGL.template = _template;
            return Success(_template);
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.WebGL.template = _oldTemplate;
        }
    }
}