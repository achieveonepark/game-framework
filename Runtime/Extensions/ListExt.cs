using System;
using System.Collections.Generic;

namespace GameFramework
{

	public static class ListExt
	{
		private static Random _random = new Random();


		public static void Add<T>(this List<T> self, IEnumerable<T> collection)
		{
			self.AddRange(collection);
		}


		public static void AddRange<T>(this List<T> list, params T[] collection)
		{
			list.AddRange(collection);
		}


		public static void AddRange<T>(this List<T> list, params IList<T>[] collectionList)
		{
			for (int i = 0; i < collectionList.Length; i++)
			{
				list.AddRange(collectionList[i]);
			}
		}


		public static void Set<T>(this List<T> list, IEnumerable<T> collection)
		{
			list.Clear();
			list.AddRange(collection);
		}


		public static void Set<T>(this List<T> list, params T[] collection)
		{
			list.Clear();
			list.AddRange(collection);
		}


		public static void Sort<T>(this List<T> self, Comparison<T> comparison)
		{
			self.Sort(comparison);
		}


		public static void Sort<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector)
			where TResult : IComparable
		{
			self.Sort((x, y) => selector(x).CompareTo(selector(y)));
		}


		public static void SortDescending<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector)
			where TResult : IComparable
		{
			self.Sort((x, y) => selector(y).CompareTo(selector(x)));
		}


		public static void Sort<TSource, TResult1, TResult2>(this List<TSource> self, Func<TSource, TResult1> selector1,
			Func<TSource, TResult2> selector2) where TResult1 : IComparable where TResult2 : IComparable
		{
			self.Sort((x, y) =>
			{
				var result = selector1(x).CompareTo(selector1(y));
				return result != 0 ? result : selector2(x).CompareTo(selector2(y));
			});
		}


		public static void SortDescending<TSource, TResult1, TResult2>(this List<TSource> self,
			Func<TSource, TResult1> selector1, Func<TSource, TResult2> selector2)
			where TResult1 : IComparable where TResult2 : IComparable
		{
			self.Sort((x, y) =>
			{
				var result = selector1(y).CompareTo(selector1(x));
				return result != 0 ? result : selector2(x).CompareTo(selector2(y));
			});
		}


		public static List<T> Shuffle<T>(this List<T> self)
		{
			int n = self.Count;
			while (1 < n)
			{
				n--;
				int k = _random.Next(n + 1);
				var tmp = self[k];
				self[k] = self[n];
				self[n] = tmp;
			}

			return self;
		}


		public static void Remove<T>(this List<T> self, Predicate<T> match)
		{
			var index = self.FindIndex(match);
			if (index == -1) return;
			self.RemoveAt(index);
		}


		public static void InsertFirst<T>(this List<T> self, T item)
		{
			self.Insert(0, item);
		}


		public static void RemoveSince<T>(this List<T> self, int count)
		{
			while (count <= self.Count)
			{
				self.RemoveAt(self.Count - 1);
			}
		}


		public static void Fill<T>(this List<T> self, int startIndex, int endIndex, T value)
		{
			for (int i = startIndex; i < endIndex; i++)
			{
				self.Add(value);
			}
		}


		public static void SetSize<T>(this List<T> self, int size)
		{
			if (self.Count <= size) return;
			self.RemoveRange(size, self.Count - size);
		}
	}
}