using System.Diagnostics;

namespace GameFramework
{

	public static class DebugUtils
	{

		[Conditional("DEBUG_LOG")]
		public static void Break()
		{
			UnityEngine.Debug.Break();
		}


		[Conditional("DEBUG_LOG")]
		public static void Log(object message)
		{
			UnityEngine.Debug.Log(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Log(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void LogFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(context, format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void Log(bool condition, object message)
		{
			if (condition) return;
			UnityEngine.Debug.Log(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Log(bool condition, object message, UnityEngine.Object context)
		{
			if (condition) return;
			UnityEngine.Debug.Log(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void LogFormat(bool condition, string format, params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void LogFormat(bool condition, UnityEngine.Object context, string format, params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogFormat(context, format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void Warning(object message)
		{
			UnityEngine.Debug.LogWarning(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Warning(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogWarning(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void WarningFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogWarningFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void WarningFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogWarningFormat(context, format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void Warning(bool condition, object message)
		{
			if (condition) return;
			UnityEngine.Debug.LogWarning(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Warning(bool condition, object message, UnityEngine.Object context)
		{
			if (condition) return;
			UnityEngine.Debug.LogWarning(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void WarningFormat(bool condition, string format, params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogWarningFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void WarningFormat(bool condition, UnityEngine.Object context, string format,
			params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogWarningFormat(context, format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void Error(object message)
		{
			UnityEngine.Debug.LogError(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Error(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogError(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void ErrorFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogErrorFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void ErrorFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogErrorFormat(context, format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void Error(bool condition, object message)
		{
			if (condition) return;
			UnityEngine.Debug.LogError(message);
		}


		[Conditional("DEBUG_LOG")]
		public static void Error(bool condition, object message, UnityEngine.Object context)
		{
			if (condition) return;
			UnityEngine.Debug.LogError(message, context);
		}


		[Conditional("DEBUG_LOG")]
		public static void ErrorFormat(bool condition, string format, params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogErrorFormat(format, args);
		}


		[Conditional("DEBUG_LOG")]
		public static void ErrorFormat(bool condition, UnityEngine.Object context, string format, params object[] args)
		{
			if (condition) return;
			UnityEngine.Debug.LogErrorFormat(context, format, args);
		}
	}
}