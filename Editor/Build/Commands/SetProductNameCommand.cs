using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 프로덕트 이름을 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetProductNameCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetProductNameCommand);

        private readonly string _productName;
        private string _oldProductName;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetProductNameCommand(string productName)
        {
            _productName = productName;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldProductName = PlayerSettings.productName;
            PlayerSettings.productName = _productName;
            return Success(_productName);
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.productName = _oldProductName;
        }
    }
}