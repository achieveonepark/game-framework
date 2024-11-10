using System;
using System.Linq;

namespace GameFramework
{

	public static class EnumUtils
	{
		private static readonly Random _random = new Random(); // 乱数


		public static T RandomAt<T>(params T[] collection)
		{
			return collection
					.OrderBy(c => _random.Next())
					.FirstOrDefault()
				;
		}


		public static T Random<T>()
		{
			return Enum.GetValues(typeof(T))
					.Cast<T>()
					.OrderBy(c => _random.Next())
					.FirstOrDefault()
				;
		}


		public static int GetLength<T>()
		{
			return Enum.GetValues(typeof(T)).Length;
		}


		public static T Parse<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}


		public static T Parse<T>(string value, bool ignoreCase)
		{
			return (T)Enum.Parse(typeof(T), value, ignoreCase);
		}


		public static bool TryParse<T>(string value, out T result)
		{
			return TryParse(value, true, out result);
		}


		public static bool TryParse<T>(string value, bool ignoreCase, out T result)
		{
			try
			{
				result = (T)Enum.Parse(typeof(T), value, ignoreCase);
				return true;
			}
			catch
			{
				result = default(T);
				return false;
			}
		}


		public static bool IsEnum<T>(string value)
		{
			T result;
			return TryParse<T>(value, out result);
		}


		public static T[] GetValues<T>()
		{
			return Enum.GetValues(typeof(T)) as T[];
		}


		public static T ToObject<T>(int value)
		{
			return (T)Enum.ToObject(typeof(T), value);
		}
	}
}