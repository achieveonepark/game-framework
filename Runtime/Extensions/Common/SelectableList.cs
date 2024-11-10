using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{

	public class SelectableList<T> : IList<T>, IDisposable
	{
		private readonly List<T> _list; // リスト


		public T this[int index]
		{
			get { return _list[index]; }
			set
			{
				_list[index] = value;
				mChanged.Call();
			}
		}


		public int Count
		{
			get { return _list.Count; }
		}


		public bool IsReadOnly
		{
			get { return false; }
		}


#pragma warning disable 0067
		public Action mChanged;
#pragma warning restore 0067


		public void Dispose()
		{
			mChanged = null;
		}


		public SelectableList()
		{
			_list = new List<T>();
		}


		public SelectableList(params T[] collection)
		{
			_list = new List<T>(collection);
		}


		public SelectableList(IList<T> collection)
		{
			_list = new List<T>(collection);
		}


		public void Set(params T[] collection)
		{
			_list.Set(collection);
			mChanged.Call();
		}


		public void Set(IList<T> collection)
		{
			_list.Set(collection);
			mChanged.Call();
		}


		public void SetWithoutCallback(params T[] collection)
		{
			_list.Set(collection);
		}


		public void SetWithoutCallback(IList<T> collection)
		{
			_list.Set(collection);
		}


		public T[] ToArray()
		{
			return _list.ToArray();
		}


		public List<T> ToList()
		{
			return _list.ToList();
		}


		public int IndexOf(T item)
		{
			return _list.IndexOf(item);
		}


		public void Insert(int index, T item)
		{
			_list.Insert(index, item);
			mChanged.Call();
		}


		public void RemoveAt(int index)
		{
			_list.RemoveAt(index);
			mChanged.Call();
		}


		public void Add(T item)
		{
			_list.Add(item);
			mChanged.Call();
		}


		public void AddWithoutCallback(T item)
		{
			_list.Add(item);
		}


		public void Clear()
		{
			_list.Clear();
			mChanged.Call();
		}


		public void ClearWithoutCallback()
		{
			_list.Clear();
		}


		public bool Contains(T item)
		{
			return _list.Contains(item);
		}


		public void CopyTo(T[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}


		public bool Remove(T item)
		{
			var result = _list.Remove(item);
			mChanged.Call();
			return result;
		}


		public void Remove(Predicate<T> match)
		{
			RemoveWithoutCallback(match);
			mChanged.Call();
		}


		public void RemoveWithoutCallback(Predicate<T> match)
		{
			_list.Remove(match);
		}


		public int RemoveAll(Predicate<T> match)
		{
			var result = RemoveAllWithoutCallback(match);
			mChanged.Call();
			return result;
		}


		public int RemoveAllWithoutCallback(Predicate<T> match)
		{
			return _list.RemoveAll(match);
		}


		public void AddOrRemove(T item)
		{
			if (Contains(item))
			{
				Remove(item);
			}
			else
			{
				Add(item);
			}
		}


		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _list.GetEnumerator();
		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}
	}
}