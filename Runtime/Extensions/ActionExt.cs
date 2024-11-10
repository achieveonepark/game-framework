using System;

namespace GameFramework
{

	public static class ActionExt
	{

		public static void Call(this Action action)
		{
			if (action != null)
			{
				action();
			}
		}


		public static void Call<T>(this Action<T> action, T arg)
		{
			if (action != null)
			{
				action(arg);
			}
		}


		public static void Call<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			if (action != null)
			{
				action(arg1, arg2);
			}
		}


		public static void Call<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			if (action != null)
			{
				action(arg1, arg2, arg3);
			}
		}


		public static void Call<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (action != null)
			{
				action(arg1, arg2, arg3, arg4);
			}
		}


		public static void CallOrDefault(this Action self, Action defaultValue)
		{
			if (self == null)
			{
				defaultValue();
				return;
			}

			self();
		}


		public static void CallOrDefault<T>(this Action<T> self, T arg, Action defaultValue)
		{
			if (self == null)
			{
				defaultValue();
				return;
			}

			self(arg);
		}


		public static void CallOrDefault<T1, T2>(this Action<T1, T2> self, T1 arg1, T2 arg2, Action defaultValue)
		{
			if (self == null)
			{
				defaultValue();
				return;
			}

			self(arg1, arg2);
		}
	}
}