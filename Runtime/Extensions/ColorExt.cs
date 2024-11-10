using UnityEngine;

namespace GameFramework
{
	public static class ColorExt
	{
		public static string EncodeColor(this Color self)
		{
			int i = 0xFFFFFF & (self.ToInt() >> 8);
			return i.DecimalToHex();
		}


		public static int ToInt(this Color self)
		{
			int result = 0;
			result |= Mathf.RoundToInt(self.r * 255f) << 24;
			result |= Mathf.RoundToInt(self.g * 255f) << 16;
			result |= Mathf.RoundToInt(self.b * 255f) << 8;
			result |= Mathf.RoundToInt(self.a * 255f);
			return result;
		}
	}
}