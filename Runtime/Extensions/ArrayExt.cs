using System;
using System.Collections.ObjectModel;

namespace GameFramework
{

	public static class ArrayExt
	{
		private static Random _random = new Random();


		public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly(array);
		}


		public static void Clear(this Array array)
		{
			Array.Clear(array, 0, array.Length);
		}


		public static void Clear(this Array array, int index)
		{
			Array.Clear(array, index, array.Length);
		}


		public static void Clear(this Array array, int index, int length)
		{
			Array.Clear(array, index, length);
		}


		public static bool Exists<T>(this T[] array, Predicate<T> match)
		{
			return Array.Exists(array, match);
		}


		public static T Find<T>(this T[] array, Predicate<T> match)
		{
			return Array.Find(array, match);
		}


		public static T[] FindAll<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindAll(array, match);
		}


		public static int FindIndex<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindIndex(array, match);
		}


		public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
		{
			return Array.FindIndex(array, startIndex, match);
		}


		public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
		{
			return Array.FindIndex(array, startIndex, count, match);
		}


		public static T FindLast<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindLast(array, match);
		}


		public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
		{
			return Array.FindLastIndex(array, match);
		}


		public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match)
		{
			return Array.FindLastIndex(array, startIndex, match);
		}


		public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
		{
			return Array.FindLastIndex(array, startIndex, count, match);
		}


		public static void ForEach<T>(this T[] array, Action<T> action)
		{
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}


		public static void ForEach<T>(this T[] array, Action<T, int> action)
		{
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i], i);
			}
		}


		public static int IndexOf<T>(this T[] array, T value)
		{
			return Array.IndexOf(array, value);
		}


		public static int IndexOf(this Array array, Object value)
		{
			return Array.IndexOf(array, value);
		}


		public static int IndexOf<T>(this T[] array, T value, int startIndex)
		{
			return Array.IndexOf(array, value, startIndex);
		}


		public static int IndexOf(this Array array, Object value, int startIndex)
		{
			return Array.IndexOf(array, value, startIndex);
		}


		public static int IndexOf<T>(this T[] array, T value, int startIndex, int count)
		{
			return Array.IndexOf(array, value, startIndex, count);
		}


		public static int IndexOf(this Array array, Object value, int startIndex, int count)
		{
			return Array.IndexOf(array, value, startIndex, count);
		}


		public static int LastIndexOf<T>(this T[] array, T value)
		{
			return Array.LastIndexOf(array, value);
		}


		public static int LastIndexOf(this Array array, Object value)
		{
			return Array.LastIndexOf(array, value);
		}


		public static int LastIndexOf<T>(this T[] array, T value, int startIndex)
		{
			return Array.LastIndexOf(array, value, startIndex);
		}


		public static int LastIndexOf(this Array array, Object value, int startIndex)
		{
			return Array.LastIndexOf(array, value, startIndex);
		}


		public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count)
		{
			return Array.LastIndexOf(array, value, startIndex, count);
		}


		public static int LastIndexOf(this Array array, Object value, int startIndex, int count)
		{
			return Array.LastIndexOf(array, value, startIndex, count);
		}


		public static T[] Reverse<T>(this T[] array)
		{
			Array.Reverse(array);
			return array;
		}


		public static T[] Reverse<T>(this T[] array, int index, int length)
		{
			Array.Reverse(array, index, length);
			return array;
		}


		public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
		{
			return Array.TrueForAll(array, match);
		}


		public static T First<T>(this T[] array)
		{
			return array[0];
		}


		public static T Last<T>(this T[] array)
		{
			return array[array.Length - 1];
		}


		public static T ElementAtRandom<T>(this T[] array)
		{
			return array.IsNotNullOrEmpty() ? array[UnityEngine.Random.Range(0, array.Length)] : default(T);
		}


		public static T[] Shuffle<T>(this T[] array)
		{
			int n = array.Length;
			while (1 < n)
			{
				n--;
				int k = _random.Next(n + 1);
				var tmp = array[k];
				array[k] = array[n];
				array[n] = tmp;
			}

			return array;
		}


		public static void FindIndex<T>(this T[] array, Predicate<T> match, Action<int> act)
		{
			var index = Array.FindIndex(array, match);
			if (index == -1)
			{
				return;
			}

			act(index);
		}


		public static void Sort<T>(this T[] array)
		{
			Array.Sort(array);
		}


		public static void Sort<T>(this T[] array, Comparison<T> comparison)
		{
			Array.Sort(array, comparison);
		}


		public static void Sort<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector)
			where TResult : IComparable
		{
			Array.Sort(array, (x, y) => selector(x).CompareTo(selector(y)));
		}


		public static void SortDescending<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector)
			where TResult : IComparable
		{
			Array.Sort(array, (x, y) => selector(y).CompareTo(selector(x)));
		}

		public static void Sort<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector1,
			Func<TSource, TResult> selector2) where TResult : IComparable
		{
			Array.Sort(array, (x, y) =>
			{
				var result = selector1(x).CompareTo(selector1(y));
				return result != 0 ? result : selector2(x).CompareTo(selector2(y));
			});
		}
	}
}