using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 지정된 빌드 명령을 순서대로 실행하는 클래스
    /// </summary>
    public class BuildProcess : IDisposable, IEnumerable
    {
        private readonly List<IBuildCommand> m_commands = new();

        /// <summary>
        /// 빌드 명령을 추가합니다
        /// </summary>
        public BuildProcess Add(IBuildCommand command)
        {
            if (command == null) return this;
            m_commands.Add(command);
            return this;
        }

        /// <summary>
        /// 모든 빌드 명령을 제거합니다
        /// </summary>
        public void Dispose()
        {
            m_commands.Clear();
        }

        /// <summary>
        /// 모든 빌드 명령을 순서대로 실행합니다
        /// </summary>
        public virtual BuildCommandResult Run()
        {
            var stringBuilder     = new StringBuilder();
            var currentIndex      = -1;
            var processResultType = BuildCommandResultType.SUCCESS;

            stringBuilder.AppendLine("--------------------[ Build Start ]--------------------");
            stringBuilder.AppendLine();

            for (var i = 0; i < m_commands.Count; i++)
            {
                var command = m_commands[i];
                var tag     = command.Tag;

                stringBuilder.AppendLine($"[{GetNowTimeString()}][{tag}] Start");

                var commandResult     = command.Run();
                var commandResultType = commandResult.Type;

                stringBuilder.AppendLine($"[{GetNowTimeString()}][{tag}] {commandResultType.ToUpperCamel()} : {commandResult.Message}");

                if (processResultType < commandResultType)
                {
                    processResultType = commandResultType;
                }

                currentIndex = i;

                if (commandResultType == BuildCommandResultType.ERROR) break;
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("--------------------[ Build End ]--------------------");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("--------------------[ Clean Up Start ]--------------------");
            stringBuilder.AppendLine();

            for (var i = currentIndex; 0 <= i; i--)
            {
                var command = m_commands[i];
                var tag     = command.Tag;
                stringBuilder.AppendLine($"[{GetNowTimeString()}][{tag}] Clean Up");
                command.CleanUp();
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("--------------------[ Clean Up End ]--------------------");

            return new BuildCommandResult
            (
                tag: string.Empty,
                type: processResultType,
                message: stringBuilder.ToString()
            );
        }

        /// <summary>
        /// 현재 시각을 나타내는 문자열을 반환합니다
        /// </summary>
        private string GetNowTimeString()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 컬렉션을 반복 처리하는 열거자를 반환합니다
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
