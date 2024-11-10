using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{

	public static class DictionaryExt
	{

		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key,
			TValue defaultValue = default(TValue))
		{
			TValue value;
			return self.TryGetValue(key, out value) ? value : defaultValue;
		}


		public static Hashtable ToHashtable<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			var result = new Hashtable();
			foreach (var n in self)
			{
				result[n.Key] = n.Value;
			}

			return result;
		}


		public static TValue ElementAtRandom<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			return self.ElementAt(UnityEngine.Random.Range(0, self.Count)).Value;
		}


		public static void ClearIfNotNull<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			if (self == null) return;
			self.Clear();
		}
	}
}