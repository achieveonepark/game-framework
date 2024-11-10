using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// Android Keystore 정보를 설정하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class SetAndroidKeystoreCommand : BuildCommandBase
    {
        public override string Tag => nameof(SetAndroidKeystoreCommand);

        private readonly string _keystoreName;
        private readonly string _keystorePass;
        private readonly string _keyaliasName;
        private readonly string _keyaliasPass;

        private string _oldKeystoreName;
        private string _oldKeystorePass;
        private string _oldKeyaliasName;
        private string _oldKeyaliasPass;

        /// <summary>
        /// 생성자
        /// </summary>
        public SetAndroidKeystoreCommand
        (
            string keystoreName,
            string keystorePass,
            string keyaliasName,
            string keyaliasPass
        )
        {
            _keystoreName = keystoreName;
            _keystorePass = keystorePass;
            _keyaliasName = keyaliasName;
            _keyaliasPass = keyaliasPass;
        }

        /// <summary>
        /// 빌드 시 호출
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            _oldKeystoreName = PlayerSettings.Android.keystoreName;
            _oldKeystorePass = PlayerSettings.Android.keystorePass;
            _oldKeyaliasName = PlayerSettings.Android.keyaliasName;
            _oldKeyaliasPass = PlayerSettings.Android.keyaliasPass;

            PlayerSettings.Android.keystoreName = _keystoreName;
            PlayerSettings.Android.keystorePass = _keystorePass;
            PlayerSettings.Android.keyaliasName = _keyaliasName;
            PlayerSettings.Android.keyaliasPass = _keyaliasPass;

            return Success(_keystoreName);
        }

        /// <summary>
        /// 정리
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.Android.keystoreName = _oldKeystoreName;
            PlayerSettings.Android.keystorePass = _oldKeystorePass;
            PlayerSettings.Android.keyaliasName = _oldKeyaliasName;
            PlayerSettings.Android.keyaliasPass = _oldKeyaliasPass;
        }
    }
}