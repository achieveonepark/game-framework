using System;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    public static class GameLog
    {
        private static IGameLog _log;

        public static void SetLogLevel(LogLevel level) => _log.LogLevel = level;
        public static void Debug(string message) => LogBase(LogLevel.Debug, message);
        public static void Info(string message) => LogBase(LogLevel.Info, message);
        public static void Warning(string message) => LogBase(LogLevel.Warning, message);
        public static void Error(string message) => LogBase(LogLevel.Error, message);
        public static Exception Fatal(Exception exception) => throw LogBase(LogLevel.Fatal, exception.Message);

        public static Exception LogBase(LogLevel level, string message)
        {
            if (_log == null)
            {
                _log = new GLog();
            }
            
            if(_log.LogLevel < level)
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