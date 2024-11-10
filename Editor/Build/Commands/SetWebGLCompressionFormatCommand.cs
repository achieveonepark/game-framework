using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// WebGL 압축 타입을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetWebGLCompressionFormatCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetWebGLCompressionFormatCommand);

        private readonly WebGLCompressionFormat _format;
        private WebGLCompressionFormat _oldFormat;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetWebGLCompressionFormatCommand(WebGLCompressionFormat format)
        {
            _format = format;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldFormat = PlayerSettings.WebGL.compressionFormat;
            PlayerSettings.WebGL.compressionFormat = _format;
            return Success(_format.ToString());
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.WebGL.compressionFormat = _oldFormat;
        }
    }
}