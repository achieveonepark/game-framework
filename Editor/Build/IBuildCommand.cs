namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 빌드 명령의 인터페이스
    /// </summary>
    public interface IBuildCommand
    {
        /// <summary>
        /// 태그를 반환합니다
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// 실행할 때 호출됩니다
        /// </summary>
        BuildCommandResult Run();

        /// <summary>
        /// 정리할 때 호출됩니다
        /// </summary>
        void CleanUp();
    }
}