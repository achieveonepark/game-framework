using UnityEngine;

namespace GameFramework
{

	public static class FloatExt
	{

		public static bool SafeEquals(this float self, float obj, float threshold = 0.001f)
		{
			return Mathf.Abs(self - obj) <= threshold;
		}


		public static bool IsValidated(this float self)
		{
			return !float.IsInfinity(self) && !float.IsNaN(self);
		}


		public static float GetValueOrDefault(this float self, float defaultValue = 0)
		{
			if (float.IsInfinity(self) || float.IsNaN(self))
			{
				return defaultValue;
			}

			return self;
		}


		public static float Clamp(this float value, float min, float max)
		{
			return Mathf.Clamp(value, min, max);
		}
	}
}