using System;

namespace GameFramework
{

	public static class TimeSpanExt
	{

		public static DateTime ToDateTime(this TimeSpan self)
		{
			return new DateTime(self.Ticks);
		}


		public static string ToPattern(this TimeSpan self)
		{
			return self.ToDateTime().ToPattern();
		}


		public static string ToShortDatePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToShortDatePattern();
		}


		public static string ToLongDatePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToLongDatePattern();
		}


		public static string ToFullDateTimePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToFullDateTimePattern();
		}


		public static string ToMiddleDateTimePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToMiddleDateTimePattern();
		}


		public static string ToShortTimePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToShortTimePattern();
		}


		public static string ToLongTimePattern(this TimeSpan self)
		{
			return self.ToDateTime().ToLongTimePattern();
		}
	}
}