namespace GameFramework.Editor.Build
{
    public abstract class BuildCommandBase : IBuildCommand
    {
        /// <summary>
        /// 태그를 반환합니다
        /// </summary>
        public abstract string Tag { get; }

        /// <summary>
        /// 실행될 때 호출됩니다
        /// </summary>
        public BuildCommandResult Run()
        {
            return DoRun();
        }

        /// <summary>
        /// 파생 클래스에서 실행 시의 처리를 작성합니다
        /// </summary>
        protected abstract BuildCommandResult DoRun();

        /// <summary>
        /// 정리할 때 호출됩니다
        /// </summary>
        public void CleanUp()
        {
            DoCleanUp();
        }

        /// <summary>
        /// 파생 클래스에서 정리 작업을 작성합니다
        /// </summary>
        protected virtual void DoCleanUp()
        {
        }

        /// <summary>
        /// 실행 결과가 성공일 때 파생 클래스에서 호출합니다
        /// </summary>
        protected BuildCommandResult Success()
        {
            return Success(string.Empty);
        }

        /// <summary>
        /// 실행 결과가 경고일 때 파생 클래스에서 호출합니다
        /// </summary>
        protected BuildCommandResult Warning()
        {
            return Warning(string.Empty);
        }

        /// <summary>
        /// 실행 결과가 오류일 때 파생 클래스에서 호출합니다
        /// </summary>
        protected BuildCommandResult Error()
        {
            return Error(string.Empty);
        }

        /// <summary>
        /// <para>실행 결과가 성공일 때 파생 클래스에서 호출합니다</para>
        /// <para>실행 결과에 메시지를 부여할 수 있습니다</para>
        /// </summary>
        protected BuildCommandResult Success(string message)
        {
            return new(Tag, BuildCommandResultType.SUCCESS, message);
        }

        /// <summary>
        /// <para>실행 결과가 경고일 때 파생 클래스에서 호출합니다</para>
        /// <para>실행 결과에 메시지를 부여할 수 있습니다</para>
        /// </summary>
        protected BuildCommandResult Warning(string message)
        {
            return new(Tag, BuildCommandResultType.WARNING, message);
        }

        /// <summary>
        /// <para>실행 결과가 오류일 때 파생 클래스에서 호출합니다</para>
        /// <para>실행 결과에 메시지를 부여할 수 있습니다</para>
        /// </summary>
        protected BuildCommandResult Error(string message)
        {
            return new(Tag, BuildCommandResultType.ERROR, message);
        }
    }
}