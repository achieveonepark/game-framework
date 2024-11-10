using System.Diagnostics;
using System.IO;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 지정된 파일이 존재하는 폴더를 여는 명령을 관리하는 클래스
    /// </summary>
    public sealed class OpenFolderCommand : BuildCommandBase
    {
        public override string Tag => nameof(OpenFolderCommand);

        private readonly string _locationPathName;

        /// <summary>
        /// 생성자
        /// </summary>
        public OpenFolderCommand(string locationPathName)
        {
            _locationPathName = locationPathName;
        }

        /// <summary>
        /// 실행할 때 호출됩니다
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var unityProjectPath = Directory.GetCurrentDirectory();
            var filePath = unityProjectPath + "\\" + _locationPathName;
            var folderPath = Path.GetDirectoryName(filePath);

            if (string.IsNullOrWhiteSpace(folderPath)) return Warning(folderPath);

            Process.Start(folderPath);

            return Success(folderPath);
        }
    }
}