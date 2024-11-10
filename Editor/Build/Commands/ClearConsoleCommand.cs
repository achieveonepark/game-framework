using System.Reflection;
using UnityEditor;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 콘솔 창을 지우는 명령을 관리하는 클래스
    /// </summary>
    public sealed class ClearConsoleCommand : BuildCommandBase
    {
        public override string Tag => nameof(ClearConsoleCommand);

        /// <summary>
        /// 실행할 때 호출됩니다
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            const BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;

            var assembly = Assembly.GetAssembly(typeof(EditorApplication));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var methodInfo = type.GetMethod("Clear", bindingAttr);

            methodInfo.Invoke(null, null);

            return Success();
        }
    }
}