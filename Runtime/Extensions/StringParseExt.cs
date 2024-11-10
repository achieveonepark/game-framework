using System;

namespace GameFramework
{

	public static class StringParseExt
	{

		public static sbyte ToSByte(this string s)
		{
			return sbyte.Parse(s);
		}


		public static sbyte? ToSByteOrNull(this string s)
		{
			sbyte result;
			if (sbyte.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static sbyte ToSByteOrDefault(this string s, sbyte defaultValue = default(sbyte))
		{
			sbyte result;
			if (sbyte.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsSByte(this string s)
		{
			sbyte result;
			return sbyte.TryParse(s, out result);
		}


		public static byte ToByte(this string s)
		{
			return byte.Parse(s);
		}


		public static byte? ToByteOrNull(this string s)
		{
			byte result;
			if (byte.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static byte ToByteOrDefault(this string s, byte defaultValue = default(byte))
		{
			byte result;
			if (byte.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsByte(this string s)
		{
			byte result;
			return byte.TryParse(s, out result);
		}


		public static char ToChar(this string s)
		{
			return char.Parse(s);
		}


		public static char? ToCharOrNull(this string s)
		{
			char result;
			if (char.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static char ToCharOrDefault(this string s, char defaultValue = default(char))
		{
			char result;
			if (char.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsChar(this string s)
		{
			char result;
			return char.TryParse(s, out result);
		}


		public static short ToShort(this string s)
		{
			return short.Parse(s);
		}


		public static short? ToShortOrNull(this string s)
		{
			short result;
			if (short.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static short ToShortOrDefault(this string s, short defaultValue = default(short))
		{
			short result;
			if (short.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsShort(this string s)
		{
			short result;
			return short.TryParse(s, out result);
		}


		public static ushort ToUShort(this string s)
		{
			return ushort.Parse(s);
		}


		public static ushort? ToUShortOrNull(this string s)
		{
			ushort result;
			if (ushort.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static ushort ToUShortOrDefault(this string s, ushort defaultValue = default(ushort))
		{
			ushort result;
			if (ushort.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsUShort(this string s)
		{
			ushort result;
			return ushort.TryParse(s, out result);
		}


		public static int ToInt(this string s)
		{
			return int.Parse(s);
		}


		public static int? ToIntOrNull(this string s)
		{
			int result;
			if (int.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static int ToIntOrDefault(this string s, int defaultValue = default(int))
		{
			int result;
			if (int.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsInt(this string s)
		{
			int result;
			return int.TryParse(s, out result);
		}


		public static uint ToUInt(this string s)
		{
			return uint.Parse(s);
		}


		public static uint? ToUIntOrNull(this string s)
		{
			uint result;
			if (uint.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static uint ToUIntOrDefault(this string s, uint defaultValue = default(uint))
		{
			uint result;
			if (uint.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsUInt(this string s)
		{
			uint result;
			return uint.TryParse(s, out result);
		}


		public static long ToLong(this string s)
		{
			return long.Parse(s);
		}


		public static long? ToLongOrNull(this string s)
		{
			long result;
			if (long.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static long ToLongOrDefault(this string s, long defaultValue = default(long))
		{
			long result;
			if (long.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsLong(this string s)
		{
			long result;
			return long.TryParse(s, out result);
		}


		public static ulong ToULong(this string s)
		{
			return ulong.Parse(s);
		}


		public static ulong? ToULongOrNull(this string s)
		{
			ulong result;
			if (ulong.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static ulong ToULongOrDefault(this string s, ulong defaultValue = default(ulong))
		{
			ulong result;
			if (ulong.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsULong(this string s)
		{
			ulong result;
			return ulong.TryParse(s, out result);
		}


		public static float ToFloat(this string s)
		{
			return float.Parse(s);
		}


		public static float? ToFloatOrNull(this string s)
		{
			float result;
			if (float.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static float ToFloatOrDefault(this string s, float defaultValue = default(float))
		{
			float result;
			if (float.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsFloat(this string s)
		{
			float result;
			return float.TryParse(s, out result);
		}


		public static double ToDouble(this string s)
		{
			return double.Parse(s);
		}


		public static double? ToDoubleOrNull(this string s)
		{
			double result;
			if (double.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static double ToDoubleOrDefault(this string s, double defaultValue = default(double))
		{
			double result;
			if (double.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsDouble(this string s)
		{
			double result;
			return double.TryParse(s, out result);
		}


		public static decimal ToDecimal(this string s)
		{
			return decimal.Parse(s);
		}


		public static decimal? ToDecimalOrNull(this string s)
		{
			decimal result;
			if (decimal.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static decimal ToDecimalOrDefault(this string s, decimal defaultValue = default(decimal))
		{
			decimal result;
			if (decimal.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsDecimal(this string s)
		{
			decimal result;
			return decimal.TryParse(s, out result);
		}


		public static DateTime ToDateTime(this string s)
		{
			return DateTime.Parse(s);
		}


		public static DateTime? ToDateTimeOrNull(this string s)
		{
			DateTime result;
			if (DateTime.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static DateTime ToDateTimeOrDefault(this string s, DateTime defaultValue = default(DateTime))
		{
			DateTime result;
			if (DateTime.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsDateTime(this string s)
		{
			DateTime result;
			return DateTime.TryParse(s, out result);
		}


		public static bool ToBoolean(this string s)
		{
			return bool.Parse(s);
		}


		public static bool? ToBooleanOrNull(this string s)
		{
			bool result;
			if (bool.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static bool ToBooleanOrDefault(this string s, bool defaultValue = default(bool))
		{
			bool result;
			if (bool.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsBoolean(this string s)
		{
			bool result;
			return bool.TryParse(s, out result);
		}


		public static T ToEnum<T>(this string s) where T : struct
		{
			return EnumUtils.Parse<T>(s);
		}


		public static T? ToEnumOrNull<T>(this string s) where T : struct
		{
			T result;
			if (EnumUtils.TryParse(s, out result))
			{
				return result;
			}

			return null;
		}


		public static T ToEnumOrDefault<T>(this string s, T defaultValue = default(T)) where T : struct
		{
			T result;
			if (EnumUtils.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}


		public static bool IsEnum<T>(string value) where T : struct
		{
			T result;
			return EnumUtils.TryParse<T>(value, out result);
		}
	}
}