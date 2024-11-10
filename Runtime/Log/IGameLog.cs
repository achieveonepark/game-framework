namespace GameFramework
{
    public interface IGameLog
    {
        LogLevel LogLevel { get; set; }
        void Log(LogLevel level, object message);
    }
}