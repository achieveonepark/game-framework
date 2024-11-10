using System;

namespace GameFramework
{

	public static class DateTimeExt
	{

		public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


		public static string ToPattern(this DateTime self)
		{
			return self.ToString("yyyy/MM/dd HH:mm:ss");
		}


		public static string ToShortDatePattern(this DateTime self)
		{
			return self.ToString("yyyy/MM/dd");
		}


		public static string ToLongDatePattern(this DateTime self)
		{
			return self.ToString("yyyy年M月d日");
		}


		public static string ToFullDateTimePattern(this DateTime self)
		{
			return self.ToString("yyyy年M月d日 HH:mm:ss");
		}


		public static string ToMiddleDateTimePattern(this DateTime self)
		{
			return self.ToString("MM/dd HH:mm");
		}


		public static string ToShortTimePattern(this DateTime self)
		{
			return self.ToString("HH:mm");
		}


		public static string ToLongTimePattern(this DateTime self)
		{
			return self.ToString("HH:mm:ss");
		}


		public static uint ToUnixTime(this DateTime self)
		{
			return (uint)self.Subtract(UNIX_EPOCH).TotalSeconds;
		}


		public static DateTime FromUnixTime(this DateTime self, long unixTime)
		{
			return UNIX_EPOCH.AddSeconds(unixTime).ToLocalTime();
		}
	}
}