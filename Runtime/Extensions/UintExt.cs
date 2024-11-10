using System;

namespace GameFramework
{

	public static class UintExt
	{

		public static void Times(this uint self, Action act)
		{
			for (uint i = 0; i < self; i++)
			{
				act();
			}
		}


		public static void Times(this uint self, Action<uint> act)
		{
			for (uint i = 0; i < self; i++)
			{
				act(i);
			}
		}


		public static string ZeroFill(this uint self, int numberOfDigits)
		{
			return self.ToString("D" + numberOfDigits.ToString());
		}
	}
}