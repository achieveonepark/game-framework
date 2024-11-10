using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
	public class MultiDictionary<TKey, TValue> : IDictionary<TKey, List<TValue>>
	{
		private Dictionary<TKey, List<TValue>> _dic;


		public MultiDictionary(IEqualityComparer<TKey> comparer)
		{
			_dic = new Dictionary<TKey, List<TValue>>(comparer);
		}


		public MultiDictionary()
		{
			_dic = new Dictionary<TKey, List<TValue>>();
		}


		public void Add(TKey key, TValue value)
		{
			if (!_dic.ContainsKey(key))
			{
				_dic.Add(key, new List<TValue>());
			}

			_dic[key].Add(value);
		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dic.GetEnumerator();
		}


		public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator()
		{
			return _dic.GetEnumerator();
		}


		public int Count
		{
			get { return _dic.Count; }
		}


		public bool IsReadOnly
		{
			get { return false; }
		}


		public void Add(KeyValuePair<TKey, List<TValue>> pair)
		{
			foreach (TValue value in pair.Value)
			{
				Add(pair.Key, value);
			}
		}


		public bool Remove(KeyValuePair<TKey, List<TValue>> pair)
		{
			return Remove(pair.Key);
		}


		public void Clear()
		{
			_dic.Clear();
		}


		public List<TValue> this[TKey key]
		{
			get { return _dic[key]; }
			set { _dic[key] = value; }
		}


		public ICollection<TKey> Keys
		{
			get { return _dic.Keys; }
		}


		public ICollection<List<TValue>> Values
		{
			get { return _dic.Values; }
		}


		public void Add(TKey key, List<TValue> values)
		{
			foreach (TValue value in values)
			{
				Add(key, value);
			}
		}


		public bool Remove(TKey key)
		{
			return _dic.Remove(key);
		}


		public bool ContainsKey(TKey key)
		{
			return _dic.ContainsKey(key);
		}


		public bool TryGetValue(TKey key, out List<TValue> value)
		{
			return _dic.TryGetValue(key, out value);
		}

		public bool Contains(KeyValuePair<TKey, List<TValue>> item)
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo(KeyValuePair<TKey, List<TValue>>[] array, int arrayIndex)
		{
			throw new System.NotImplementedException();
		}
	}
}