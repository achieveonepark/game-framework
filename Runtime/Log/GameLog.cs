using System;

namespace GameFramework
{
    public static partial class GameLog
    {
        private static IGameLog _log;

        public static void Initialize()
        {
            _log = new GLog { LogLevel = LogLevel.Debug };
        }
        
        public static void SetLogLevel(LogLevel level) => _log.LogLevel = level;
        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message) => Log(LogLevel.Error, message);
        public static Exception Fatal(string message) => throw Log(LogLevel.Fatal, message);

        public static Exception Log(LogLevel level, string message)
        {
            if (_log == null || _log.LogLevel < level)
            {
                return null;
            }

            if (level == LogLevel.Fatal)
            {
                _log.Log(level, message);
                throw new Exception(message);
            }

            _log.Log(level, message);
            return level == LogLevel.Fatal ? new Exception(message) : null;
        }
    }
}