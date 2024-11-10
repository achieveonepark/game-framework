using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{

	public static class IListExt
	{

		public static int Count<T>(this IList<T> self, Func<T, bool> predicate)
		{
			int count = 0;
			for (int i = 0; i < self.Count; i++)
			{
				if (predicate(self[i]))
				{
					count++;
				}
			}

			return count;
		}


		public static T ElementAtRandom<T>(this IList<T> self)
		{
			return self.IsEmpty() ? default(T) : self[UnityEngine.Random.Range(0, self.Count)];
		}


		public static T Dequeue<T>(this IList<T> self)
		{
			var result = self[0];
			self.RemoveAt(0);
			return result;
		}


		public static void ForEachReverse<T>(this IList<T> self, Action<T> act)
		{
			for (int i = self.Count - 1; 0 <= i; i--)
			{
				act(self[i]);
			}
		}


		public static void ForEachReverse<T>(this IList<T> self, Action<T, int> act)
		{
			for (int i = self.Count - 1; 0 <= i; i--)
			{
				act(self[i], i);
			}
		}


		public static void ForEach<T>(this IList<T> self, Action<T> act)
		{
			for (int i = 0; i < self.Count; i++)
			{
				act(self[i]);
			}
		}


		public static void ForEach<T>(this IList<T> self, Action<T, int> act)
		{
			for (int i = 0; i < self.Count; i++)
			{
				act(self[i], i);
			}
		}


		public static IList<T> FindAll<T>(this IList<T> self, Predicate<T> match)
		{
			var result = new List<T>();
			for (int i = 0; i < self.Count; i++)
			{
				if (match(self[i]))
				{
					result.Add(self[i]);
				}
			}

			return result;
		}


		public static T Find<T>(this IList<T> self, Predicate<T> match)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (match(self[i]))
				{
					return self[i];
				}
			}

			return default(T);
		}


		public static int FindIndex<T>(this IList<T> self, Predicate<T> match)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (match(self[i]))
				{
					return i;
				}
			}

			return -1;
		}


		public static T FindLast<T>(this IList<T> self, Predicate<T> match)
		{
			for (int i = self.Count - 1; 0 <= i; i--)
			{
				if (match(self[i]))
				{
					return self[i];
				}
			}

			return default(T);
		}


		public static int FindLastIndex<T>(this IList<T> self, Predicate<T> match)
		{
			for (int i = self.Count - 1; 0 <= i; i--)
			{
				if (match(self[i]))
				{
					return i;
				}
			}

			return -1;
		}

		public static T Get<T>(this IList<T> self, T match)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (match.Equals(self[i]))
				{
					return self[i];
				}
			}

			return default;
		}
		
		public static bool TryGetValueForType<T>(this IList<T> self, Type match, out T value)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (match == self[i].GetType())
				{
					value = self[i];
					return true;
				}
			}

			value = default;
			return false;
		}


		public static bool Any<T>(this IList<T> self, Func<T, bool> predicate)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (predicate(self[i]))
				{
					return true;
				}
			}

			return false;
		}


		public static bool All<T>(this IList<T> self, Func<T, bool> predicate)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (!predicate(self[i]))
				{
					return false;
				}
			}

			return true;
		}


		public static bool Contains<T>(this IList<T> self, T value)
		{
			for (int i = 0; i < self.Count; i++)
			{
				if (self[i].Equals(value))
				{
					return true;
				}
			}

			return false;
		}


		public static int Sum<T>(this IList<T> self, Func<T, int> selector)
		{
			int result = 0;

			for (int i = 0; i < self.Count; i++)
			{
				result += selector(self[i]);
			}

			return result;
		}


		public static uint Sum<T>(this IList<T> self, Func<T, uint> selector)
		{
			uint result = 0;

			for (int i = 0; i < self.Count; i++)
			{
				result += selector(self[i]);
			}

			return result;
		}


		public static int Max<T>(this IList<T> self, Func<T, int> selector)
		{
			int result = int.MinValue;

			for (int i = 0; i < self.Count; i++)
			{
				var value = selector(self[i]);

				if (result < value)
				{
					result = value;
				}
			}

			return result;
		}


		public static uint Max<T>(this IList<T> self, Func<T, uint> selector)
		{
			uint result = uint.MinValue;

			for (int i = 0; i < self.Count; i++)
			{
				var value = selector(self[i]);

				if (result < value)
				{
					result = value;
				}
			}

			return result;
		}


		public static int Min<T>(this IList<T> self, Func<T, int> selector)
		{
			int result = int.MaxValue;

			for (int i = 0; i < self.Count; i++)
			{
				var value = selector(self[i]);

				if (value < result)
				{
					result = value;
				}
			}

			return result;
		}


		public static uint Min<T>(this IList<T> self, Func<T, uint> selector)
		{
			uint result = uint.MaxValue;

			for (int i = 0; i < self.Count; i++)
			{
				var value = selector(self[i]);

				if (value < result)
				{
					result = value;
				}
			}

			return result;
		}


		public static T ElementAtOrDefault<T>(this IList<T> self, int index)
		{
			return 0 <= index && index < self.Count ? self[index] : default(T);
		}


		public static T ElementAtOrDefault<T>(this IList<T> self, int index, T defaultValue)
		{
			return 0 <= index && index < self.Count ? self[index] : defaultValue;
		}


		public static T FindMin<T>(this IList<T> self, Func<T, int> selector)
		{
			return self.Find(c => selector(c) == self.Min(selector));
		}


		public static T FindMin<T>(this IList<T> self, Func<T, uint> selector)
		{
			return self.Find(c => selector(c) == self.Min(selector));
		}


		public static T FindMax<T>(this IList<T> self, Func<T, int> selector)
		{
			return self.Find(c => selector(c) == self.Max(selector));
		}


		public static T FindMax<T>(this IList<T> self, Func<T, uint> selector)
		{
			return self.Find(c => selector(c) == self.Max(selector));
		}


		public static int Nearest
		(
			this IList<int> self,
			int target
		)
		{
			var min = self.Min(c => Math.Abs(c - target));
			return self.First(c => Math.Abs(c - target) == min);
		}


		public static int Nearest<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min(c => Math.Abs(selector(c) - target));
			return selector(self.First(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearest<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min(c => Math.Abs(selector(c) - target));
			return self.First(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestOrDefault(
			this IList<int> self,
			int target
		)
		{
			var min = self.Min(c => Math.Abs(c - target));
			return self.Find(c => Math.Abs(c - target) == min);
		}


		public static int NearestOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min(c => Math.Abs(selector(c) - target));
			return selector(self.Find(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min(c => Math.Abs(selector(c) - target));
			return self.Find(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestMoreThan
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => target < c).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.First(c => Math.Abs(c - target) == min);
		}


		public static int NearestMoreThan<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target < selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.First(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestMoreThan<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target < selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.First(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestMoreThanOrDefault
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => target < c).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.Find(c => Math.Abs(c - target) == min);
		}


		public static int NearestMoreThanOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target < selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.Find(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestMoreThanOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target < selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.Find(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestOrLess
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => c <= target).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.First(c => Math.Abs(c - target) == min);
		}


		public static int NearestOrLess<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) <= target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.First(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestOrLess<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) <= target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.First(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestOrLessOrDefault
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => c <= target).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.Find(c => Math.Abs(c - target) == min);
		}


		public static int NearestOrLessOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) <= target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.Find(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestOrLessOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) <= target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.Find(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestOrMore
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => target <= c).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.First(c => Math.Abs(c - target) == min);
		}


		public static int NearestOrMore<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target <= selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.First(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestOrMore<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target <= selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.First(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestOrMoreOrDefault
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => target <= c).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.Find(c => Math.Abs(c - target) == min);
		}


		public static int NearestOrMoreOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target <= selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.Find(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestOrMoreOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => target <= selector(c)).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.Find(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestMoreLess
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => c < target).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.First(c => Math.Abs(c - target) == min);
		}


		public static int NearestMoreLess<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) < target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.First(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestMoreLess<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) < target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.First(c => Math.Abs(selector(c) - target) == min);
		}


		public static int NearestMoreLessOrDefault
		(
			this IList<int> self,
			int target
		)
		{
			var list = self.Where(c => c < target).ToArray();
			var min = list.Min(c => Math.Abs(c - target));
			return list.Find(c => Math.Abs(c - target) == min);
		}


		public static int NearestMoreLessOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) < target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return selector(list.Find(c => Math.Abs(selector(c) - target) == min));
		}


		public static TSource FindNearestMoreLessOrDefault<TSource>
		(
			this IList<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where(c => selector(c) < target).ToArray();
			var min = list.Min(c => Math.Abs(selector(c) - target));
			return list.Find(c => Math.Abs(selector(c) - target) == min);
		}


		public static void FillBy<T>(this IList<T> list, T value, int count)
		{
			int listcount = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (i < listcount)
				{
					list[i] = value;
				}
				else
				{
					list.Add(value);
				}
			}
		}


		public static void FillBy<T>(this IList<T> list, Func<int, T> func, int count)
		{
			int listcount = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (i < listcount)
				{
					list[i] = func(i);
				}
				else
				{
					list.Add(func(i));
				}
			}
		}


		public static void Apply<T>(this IList<T> list, Func<T, int, T> func)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = func(list[i], i);
			}
		}


		public static void Apply<T>(this IList<T> list, Func<T, T> func)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = func(list[i]);
			}
		}


		public static bool IsDefinedAt<T>(IList<T> self, int index)
		{
			return index < self.Count;
		}
	}
}