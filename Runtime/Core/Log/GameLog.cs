using System;

namespace GameFramework
{
    public static class GameLog
    {
        private static IGameLog _log;
        
        static GameLog()
        {
            _log = new GLog { LogLevel = LogLevel.Debug };
        }
        
        public static void SetLog(IGameLog log)
        {
            _log = log;
        }
        
        public static void SetLogLevel(LogLevel level) => _log.LogLevel = level;
        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message) => Log(LogLevel.Error, message);
        public static Exception Fatal(Exception exception) => throw Log(LogLevel.Fatal, exception.Message);

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