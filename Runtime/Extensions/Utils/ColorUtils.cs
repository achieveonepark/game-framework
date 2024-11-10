using UnityEngine;

namespace GameFramework
{

	public static class ColorUtils
	{

		public static string DecimalToHex(this int self)
		{
			self &= 0xFFFFFF;
			return self.ToString("X6");
		}


		public static Color ToARGB(uint val)
		{
			var inv = 1f / 255f;
			var c = Color.black;
			c.a = inv * ((val >> 24) & 0xFF);
			c.r = inv * ((val >> 16) & 0xFF);
			c.g = inv * ((val >> 8) & 0xFF);
			c.b = inv * (val & 0xFF);
			return c;
		}


		public static Color ToRGBA(uint val)
		{
			var inv = 1f / 255f;
			var c = Color.black;
			c.r = inv * ((val >> 24) & 0xFF);
			c.g = inv * ((val >> 16) & 0xFF);
			c.b = inv * ((val >> 8) & 0xFF);
			c.a = inv * (val & 0xFF);
			return c;
		}


		public static Color ToRGB(uint val)
		{
			var inv = 1f / 255f;
			var c = Color.black;
			c.r = inv * ((val >> 16) & 0xFF);
			c.g = inv * ((val >> 8) & 0xFF);
			c.b = inv * (val & 0xFF);
			c.a = 1f;
			return c;
		}


		public static Color ToRGB(int val)
		{
			return ToRGB((uint)val);
		}
	}
}