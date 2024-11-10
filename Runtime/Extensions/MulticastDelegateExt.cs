using System;

namespace GameFramework
{

	public static class MulticastDelegateExt
	{

		public static int GetLengthIfNotNull(this MulticastDelegate self)
		{
			if (self == null || self.GetInvocationList() == null) return 0;
			return self.GetInvocationList().Length;
		}


		public static int GetLength(this MulticastDelegate self)
		{
			return self.GetInvocationList().Length;
		}


		public static bool IsNullOrEmpty(this MulticastDelegate self)
		{
			return self == null || self.GetInvocationList() == null || self.GetInvocationList().Length <= 0;
		}


		public static bool IsNotNullOrEmpty(this MulticastDelegate self)
		{
			return !self.IsNullOrEmpty();
		}
	}
}