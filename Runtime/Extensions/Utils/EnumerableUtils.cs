using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{

	public static class EnumerableUtils
	{

		public static IEnumerable<int> Range(int count)
		{
			return Enumerable.Range(0, count);
		}


		public static IEnumerable<int> Range(uint count)
		{
			return Enumerable.Range(0, (int)count);
		}
	}
}