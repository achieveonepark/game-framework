using System;
using UnityEngine;

namespace GameFramework
{

	public static class EnumExt
	{

		public static bool HasFlag(this Enum self, Enum flag)
		{
			if (self.GetType() != flag.GetType()) return false;

			var selfValue = Convert.ToUInt64(self);
			var flagValue = Convert.ToUInt64(flag);

			return (selfValue & flagValue) == flagValue;
		}


		public static T PrevLoop<T>(this T self)
		{
			var intValue = Convert.ToInt32(self);
			var length = EnumUtils.GetLength<T>();
			var nextValue = (intValue - 1 + length) % length;
			var enumValue = EnumUtils.ToObject<T>(nextValue);

			return enumValue;
		}


		public static T NextLoop<T>(this T self)
		{
			var intValue = Convert.ToInt32(self);
			var nextValue = (intValue + 1) % EnumUtils.GetLength<T>();
			var enumValue = EnumUtils.ToObject<T>(nextValue);

			return enumValue;
		}


		public static T Prev<T>(this T self)
		{
			var intValue = Convert.ToInt32(self);
			var nextValue = Mathf.Max(0, intValue - 1);
			var enumValue = EnumUtils.ToObject<T>(nextValue);

			return enumValue;
		}


		public static T Next<T>(this T self)
		{
			var intValue = Convert.ToInt32(self);
			var nextValue = Mathf.Min(EnumUtils.GetLength<T>(), intValue + 1);
			var enumValue = EnumUtils.ToObject<T>(nextValue);

			return enumValue;
		}
	}
}