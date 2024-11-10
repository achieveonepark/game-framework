namespace GameFramework
{

	public static class BoolExt
	{

		public static byte ToByte(this bool self)
		{
			return (byte)(self ? 1 : 0);
		}
	}
}