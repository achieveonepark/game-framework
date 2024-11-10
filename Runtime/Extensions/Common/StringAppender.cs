using System;
using System.Collections;
using System.Text;

namespace GameFramework
{

	public class StringAppender : IEnumerable
	{
		//==============================================================================
		// メンバ変数
		//==============================================================================
		private readonly StringBuilder _builder = new StringBuilder();

		//==============================================================================
		// メンバ関数
		//==============================================================================

		public void Add(string value)
		{
			_builder.AppendLine(value);
		}


		public void Add(string format, params object[] args)
		{
			_builder.AppendFormat(format, args).AppendLine();
		}


		public void Add(string format, object arg0)
		{
			_builder.AppendFormat(format, arg0).AppendLine();
		}


		public void Add(string format, object arg0, object arg1)
		{
			_builder.AppendFormat(format, arg0, arg1).AppendLine();
		}


		public void Add(string format, object arg0, object arg1, object arg2)
		{
			_builder.AppendFormat(format, arg0, arg1, arg2).AppendLine();
		}


		public override string ToString()
		{
			return _builder.ToString();
		}


		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}