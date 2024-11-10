using System;
using GameFramework;
using UnityEngine;

namespace GameFramework
{
    public class GLog : IGameLog
    {
        public LogLevel LogLevel { get; set; }

        public void Log(LogLevel level, object message)
        {
            string color = level switch
            {
                LogLevel.Info => "green",
                LogLevel.Debug => "white",
                LogLevel.Warning => "yellow",
                LogLevel.Error => "red",
                LogLevel.Fatal => "red",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };

            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    Debug.Log($"<color={color}>[{level}] {message}</color>");
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning($"<color={color}>[{level}] {message}</color>");
                    break;           
                case LogLevel.Error:
                    Debug.LogError($"<color={color}>[{level}] {message}</color>");
                    break;
                case LogLevel.Fatal:
                    throw new Exception($"<color={color}>[{level}] {message}</color>");
            }
        }
    }
}