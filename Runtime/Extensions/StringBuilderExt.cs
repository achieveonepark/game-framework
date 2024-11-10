using System.Text;

namespace GameFramework
{

	public static class StringBuilderExt
	{

		public static StringBuilder AppendLine(this StringBuilder self, string format, object arg0)
		{
			return self.AppendFormat(format, arg0).AppendLine();
		}


		public static StringBuilder AppendLine(this StringBuilder self, string format, params object[] args)
		{
			return self.AppendFormat(format, args).AppendLine();
		}


		public static StringBuilder AppendLine(this StringBuilder self, string format, object arg0, object arg1)
		{
			return self.AppendFormat(format, arg0, arg1).AppendLine();
		}


		public static StringBuilder AppendLine(this StringBuilder self, string format, object arg0, object arg1,
			object arg2)
		{
			return self.AppendFormat(format, arg0, arg1, arg2).AppendLine();
		}
	}
}