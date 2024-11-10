namespace GameFramework
{

	public static class ByteExt
	{

		public static bool ToBool(this byte self)
		{
			return self == 1;
		}


		public static string ZeroFill(this byte self, int numberOfDigits)
		{
			return self.ToString("D" + numberOfDigits.ToString());
		}
	}
}