namespace GameFramework
{

	public static class RandomUtils
	{

		public static float Value
		{
			get { return UnityEngine.Random.value; }
		}


		public static bool BoolValue
		{
			get { return UnityEngine.Random.Range(0, 2) == 0; }
		}


		public static byte Flag
		{
			get { return BoolValue.ToByte(); }
		}


		public static T Random<T>(params T[] values)
		{
			return values[UnityEngine.Random.Range(0, values.Length)];
		}


		public static int Range(int max)
		{
			return UnityEngine.Random.Range(0, max);
		}


		public static byte RangeByte(byte max)
		{
			return (byte)UnityEngine.Random.Range(0, max);
		}


		public static ushort RangeUshort(uint max)
		{
			return (ushort)UnityEngine.Random.Range(0, (int)max);
		}


		public static uint RangeUint(uint max)
		{
			return (uint)UnityEngine.Random.Range(0, (int)max);
		}


		public static ulong RangeUlong(ulong max)
		{
			return (ulong)UnityEngine.Random.Range(0, (int)max);
		}


		public static int Range(int min, int max)
		{
			return UnityEngine.Random.Range(min, max);
		}


		public static float Range(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}
	}
}