using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{
	
	public static class IEnumerableExt
	{

		private sealed class CompareSelector<T, TKey> : IEqualityComparer<T>
		{
			private readonly Func<T, TKey> _selector;


			public CompareSelector(Func<T, TKey> selector)
			{
				_selector = selector;
			}


			public bool Equals(T x, T y)
			{
				return _selector(x).Equals(_selector(y));
			}


			public int GetHashCode(T obj)
			{
				return _selector(obj).GetHashCode();
			}
		}

		private static readonly Random _random = new Random();

		
		public static void ForEach<T>( this IEnumerable<T> self, Action<T> action )
		{
			foreach ( var n in self )
			{
				action( n );
			}
		}

		
		public static void ForEach<T>( this IEnumerable<T> self, Action<T, int> action )
		{
			int index = 0;
			foreach ( var n in self )
			{
				action( n, index++ );
			}
		}

		
		public static bool IsNullOrEmpty<T>( this IEnumerable<T> self )
		{
			return self == null || !self.Any();
		}

		
		public static bool IsNotNullOrEmpty<T>( this IEnumerable<T> self )
		{
			return !self.IsNullOrEmpty();
		}

		
		public static IEnumerable<T> Pager<T>( this IEnumerable<T> self, int page, int count )
		{
			return self.Skip( page * count ).Take( count );
		}

		
		public static IEnumerable<T> DefaultIfNull<T>( this IEnumerable<T> self )
		{
			return self == null ? new T[ 0 ] : self;
		}

		
		public static IEnumerable<TSource> NotNull<TSource>( this IEnumerable<TSource> self )
		{
			return self.Where( c => c != null );
		}

		
		public static bool SequenceEqual<T, TKey>( this IEnumerable<T> source, IEnumerable<T> second, Func<T, TKey> selector )
		{
			return source.SequenceEqual( second, new CompareSelector<T, TKey>( selector ) );
		}

		
		public static IEnumerable<T> Distinct<T, TKey>( this IEnumerable<T> source, Func<T, TKey> selector )
		{
			return source.Distinct( new CompareSelector<T, TKey>( selector ) );
		}

		
		public static TSource MaxBy<TSource, TResult>
		(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector
		)
		{
			var value = source.Max( selector );
			return source.FirstOrDefault( c => selector( c ).Equals( value ) );
		}

		
		public static IEnumerable<TSource> MaxElementsBy<TSource, TResult>
		(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector
		)
		{
			var value = source.Max( selector );
			return source.Where( c => selector( c ).Equals( value ) );
		}

		
		public static TSource MinBy<TSource, TResult>
		(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector
		)
		{
			var value = source.Min( selector );
			return source.FirstOrDefault( c => selector( c ).Equals( value ) );
		}

		
		public static IEnumerable<TSource> MinElementsBy<TSource, TResult>
		(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector
		)
		{
			var value = source.Min( selector );
			return source.Where( c => selector( c ).Equals( value ) );
		}

		
		public static bool IsEmpty<TSource>( this IEnumerable<TSource> source )
		{
			return !source.Any();
		}

		
		public static IEnumerable<TSource> StartWith<TSource>
		(
			this IEnumerable<TSource> source,
			params TSource[] value
		)
		{
			foreach ( var n in value )
			{
				yield return n;
			}
			foreach ( var n in source )
			{
				yield return n;
			}
		}

		
		public static IEnumerable<TSource> Concat<TSource>
		(
			params IEnumerable<TSource>[] sources
		)
		{
			foreach ( var source in sources )
			{
				foreach ( var n in source )
				{
					yield return n;
				}
			}
		}

		
		public static IEnumerable<TSource> WhereNot<TSource, TResult>
		(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate
		)
		{
			return source.Where( c => !predicate( c ) );
		}

		
		public static int Nearest
		(
			this IEnumerable<int> self,
			int target
		)
		{
			var min = self.Min( c => Math.Abs( c - target ) );
			return self.FirstOrDefault( c => Math.Abs( c - target ) == min );
		}

		
		public static int Nearest<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min( c => Math.Abs( selector( c ) - target ) );
			return selector( self.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min ) );
		}

		
		public static TSource FindNearest<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var min = self.Min( c => Math.Abs( selector( c ) - target ) );
			return self.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min );
		}

		
		public static int NearestMoreThan
		(
			this IEnumerable<int> self,
			int target
		)
		{
			var list = self.Where( c => target < c ).ToArray();
			var min = list.Min( c => Math.Abs( c - target ) );
			return list.FirstOrDefault( c => Math.Abs( c - target ) == min );
		}

		
		public static int NearestMoreThan<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => target < selector( c ) ).ToArray();
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return selector( list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min ) );
		}

		
		public static TSource FindNearestMoreThan<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => target < selector( c ) ).ToArray();
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min );
		}

		
		public static int NearestOrLess
		(
			this IEnumerable<int> self,
			int target
		)
		{
			var list = self.Where( c => c <= target );
			var min = list.Min( c => Math.Abs( c - target ) );
			return list.FirstOrDefault( c => Math.Abs( c - target ) == min );
		}

		
		public static int NearestOrLess<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => selector( c ) <= target );
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return selector( list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min ) );
		}

		
		public static TSource FindNearestOrLess<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => selector( c ) <= target );
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min );
		}

		
		public static int NearestOrMore
		(
			this IEnumerable<int> self,
			int target
		)
		{
			var list = self.Where( c => target <= c );
			var min = list.Min( c => Math.Abs( c - target ) );
			return list.FirstOrDefault( c => Math.Abs( c - target ) == min );
		}

		
		public static int NearestOrMore<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => target <= selector( c ) );
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return selector( list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min ) );
		}

		
		public static TSource FindNearestOrMore<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => target <= selector( c ) );
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min );
		}

		
		public static int NearestMoreLess
		(
			this IEnumerable<int> self,
			int target
		)
		{
			var list = self.Where( c => c < target ).ToArray();
			var min = list.Min( c => Math.Abs( c - target ) );
			return list.FirstOrDefault( c => Math.Abs( c - target ) == min );
		}

		
		public static int NearestMoreLess<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => selector( c ) < target ).ToArray();
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return selector( list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min ) );
		}

		
		public static TSource FindNearestMoreLess<TSource>
		(
			this IEnumerable<TSource> self,
			int target,
			Func<TSource, int> selector
		)
		{
			var list = self.Where( c => selector( c ) < target ).ToArray();
			var min = list.Min( c => Math.Abs( selector( c ) - target ) );
			return list.FirstOrDefault( c => Math.Abs( selector( c ) - target ) == min );
		}

		
		public static T RandomAtWeight<T>( this IEnumerable<T> self, Func<T, int> selector )
		{
			var current = 0;
			var rate = _random.Next( 0, self.Sum( selector ) );

			var result = self.FirstOrDefault( c =>
			{
				current += selector( c );
				return rate < current;
			} );

			return result;
		}

		
		public static IEnumerable<TSource> Concat<TSource>( this IEnumerable<TSource> first, TSource second )
		{
			return first.Concat( new[] { second } );
		}

		
		public static IEnumerable<IEnumerable<TSource>> Chunks<TSource>( this IEnumerable<TSource> self, int size )
		{
			while ( self.Any() )
			{
				yield return self.Take( size );
				self = self.Skip( size );
			}
		}

		
		/// <code>
		/// foreach ( var ( index, value ) in list.WithIndex() )
		///	{
		///	    Debug.Log( index + ":" + value );
		///	}
		/// </code>
		public static IEnumerable<(int index, T value)> WithIndex<T>( this IEnumerable<T> source )
		{
			if ( source == null )
			{
				throw new ArgumentNullException( nameof( source ) );
			}

			IEnumerable<(int index, T value)> Impl()
			{
				var i = 0;
				foreach ( var value in source )
				{
					yield return ( i, value );
					i++;
				}
			}

			return Impl();
		}

		
		public static IEnumerable<T> Concat<T>( this IEnumerable<T> first, params T[] second )
		{
			return Enumerable.Concat( first, second );
		}

		
		/// <code>
		/// var list = new[]
		///	{
		///	    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
		///	};
		///
		///	var ( ok, ng ) = list.Partition( c => c % 2 == 0 );
		///
		///	foreach ( var n in ok )
		///	{
		///	}
		///
		/// foreach ( var n in ng )
		///	{
		/// }
		/// </code>
		public static Tuple<IEnumerable<T>, IEnumerable<T>> Partition<T>( this IEnumerable<T> self, Func<T, bool> predicate )
		{
			var ok = new List<T>();
			var ng = new List<T>();

			foreach ( var n in self )
			{
				if ( predicate( n ) )
				{
					ok.Add( n );
				}
				else
				{
					ng.Add( n );
				}
			}

			return Tuple.Create( ( IEnumerable<T> ) ok, ( IEnumerable<T> ) ng );
		}
		
		
		public static T RandomAt<T>( this IEnumerable<T> self )
		{
			return self.Any() 
				? self.ElementAt( UnityEngine.Random.Range( 0, self.Count() ) ) 
				: default;
		}
	}
}