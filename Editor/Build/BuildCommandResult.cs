namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 빌드 명령의 결과를 관리하는 구조체
    /// </summary>
    public readonly struct BuildCommandResult
    {
        /// <summary>
        /// 태그를 반환합니다
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// 결과 타입을 반환합니다
        /// </summary>
        public BuildCommandResultType Type { get; }

        /// <summary>
        /// 메시지를 반환합니다
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        public BuildCommandResult
        (
            string tag,
            BuildCommandResultType type,
            string message
        )
        {
            Tag = tag;
            Message = message;
            Type = type;
        }
    }
}