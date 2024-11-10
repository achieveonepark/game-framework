using System;

namespace GameFramework
{

	public static class FuncExt
	{

		public static TResult Call<TResult>(this Func<TResult> self, TResult result = default(TResult))
		{
			return self != null ? self() : result;
		}


		public static TResult Call<T, TResult>(this Func<T, TResult> self, T arg, TResult result = default(TResult))
		{
			return self != null ? self(arg) : result;
		}


		public static TResult Call<T1, T2, TResult>(this Func<T1, T2, TResult> self, T1 arg1, T2 arg2,
			TResult result = default(TResult))
		{
			return self != null ? self(arg1, arg2) : result;
		}


		public static TResult Call<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> self, T1 arg1, T2 arg2, T3 arg3,
			TResult result = default(TResult))
		{
			return self != null ? self(arg1, arg2, arg3) : result;
		}


		public static bool All(this Func<bool> self)
		{
			return self
					.GetInvocationList()
					.All(c => (bool)c.DynamicInvoke())
				;
		}


		public static bool All<T>(this Func<T, bool> self, T arg)
		{
			return self
					.GetInvocationList()
					.All(c => (bool)c.DynamicInvoke(arg))
				;
		}


		public static bool All<T1, T2>(this Func<T1, T2, bool> self, T1 arg1, T2 arg2)
		{
			return self
					.GetInvocationList()
					.All(c => (bool)c.DynamicInvoke(arg1, arg2))
				;
		}


		public static bool All<T1, T2, T3>(this Func<T1, T2, T3, bool> self, T1 arg1, T2 arg2, T3 arg3)
		{
			return self
					.GetInvocationList()
					.All(c => (bool)c.DynamicInvoke(arg1, arg2, arg3))
				;
		}


		public static bool Any(this Func<bool> self)
		{
			return self
					.GetInvocationList()
					.Any(c => (bool)c.DynamicInvoke())
				;
		}


		public static bool Any<T>(this Func<T, bool> self, T arg)
		{
			return self
					.GetInvocationList()
					.Any(c => (bool)c.DynamicInvoke(arg))
				;
		}


		public static bool Any<T1, T2>(this Func<T1, T2, bool> self, T1 arg1, T2 arg2)
		{
			return self
					.GetInvocationList()
					.Any(c => (bool)c.DynamicInvoke(arg1, arg2))
				;
		}


		public static bool Any<T1, T2, T3>(this Func<T1, T2, T3, bool> self, T1 arg1, T2 arg2, T3 arg3)
		{
			return self
					.GetInvocationList()
					.Any(c => (bool)c.DynamicInvoke(arg1, arg2, arg3))
				;
		}
	}
}