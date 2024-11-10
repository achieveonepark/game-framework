using System;
using System.Diagnostics;

namespace GameFramework
{

	public static class StopwatchExt
	{

		public static string ToPattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("yyyy/MM/dd HH:mm:ss");
		}


		public static string ToShortDatePattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("yyyy/MM/dd");
		}


		public static string ToLongDatePattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("yyyy年M月d日");
		}


		public static string ToFullStopwatchPattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("yyyy年M月d日 HH:mm:ss");
		}


		public static string ToMiddleStopwatchPattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("MM/dd HH:mm");
		}


		public static string ToShortTimePattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("HH:mm");
		}


		public static string ToLongTimePattern(this Stopwatch self)
		{
			return new DateTime(self.Elapsed.Ticks).ToString("HH:mm:ss");
		}
	}
}