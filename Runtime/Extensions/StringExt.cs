using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameFramework
{
	
	public static class StringExt
	{
		
		public static bool IsMatch( this string str, string pattern )
		{
			return Regex.IsMatch( str, pattern );
		}

		
		public static Match Match( this string str, string pattern )
		{
			return Regex.Match( str, pattern );
		}

		
		public static MatchCollection Matches( this string str, string pattern )
		{
			return Regex.Matches( str, pattern );
		}

		
		public static string ReplaceRegex( this string str, string pattern, string replacement )
		{
			return Regex.Replace( str, pattern, replacement );
		}

		
		public static string FormatWith( this string format, params Object[] args )
		{
			return string.Format( format, args );
		}

		
		public static string ConcatWith<T>( this IEnumerable<T> self, string separator )
		{
			var list = new List<string>();
			foreach ( var n in self )
			{
				list.Add( n.ToString() );
			}
			return string.Join( separator, list.ToArray() );
		}

		
		public static string ConcatWith( this IEnumerable self, string separator )
		{
			var list = new List<object>();
			foreach ( var n in self )
			{
				list.Add( n );
			}
			return string.Join( separator, list.Select( c => c.ToString() ).ToArray() );
		}

		
		public static string ConcatWith<T>( this IEnumerable<T> self, string separator, string format, IFormatProvider provider = null ) where T : IFormattable
		{
			return self.Select( x => x.ToString( format, provider ) ).Aggregate( ( a, b ) => a + separator + b );
		}

		
		public static bool IsNullOrEmpty( this string value )
		{
			return string.IsNullOrEmpty( value );
		}

		
		public static bool IsNullOrWhiteSpace( this string value )
		{
			return value == null || value.Trim() == "";
		}

		
		public static bool IsNotNullOrEmpty( this string self )
		{
			return !self.IsNullOrEmpty();
		}

		
		public static bool IsNotNullOrWhiteSpace( this string self )
		{
			return !self.IsNullOrWhiteSpace();
		}

		
		public static string DefaultIfEmpty( this string self )
		{
			return string.IsNullOrEmpty( self ) ? string.Empty : self;
		}

		
		public static string DefaultIfEmpty( this string self, string defaultValue )
		{
			return string.IsNullOrEmpty( self ) ? defaultValue : self;
		}

		
		public static string DefaultIfWhiteSpace( this string self )
		{
			return IsNullOrWhiteSpace( self ) ? string.Empty : self;
		}

		
		public static string DefaultIfWhiteSpace( this string self, string defaultValue )
		{
			return IsNullOrWhiteSpace( self ) ? defaultValue : self;
		}

		
		public static string Limit( this string self, int maxLength, string suffix = null )
		{
			if ( self.Length <= maxLength ) return self;
			return string.Concat( self.Substring( 0, maxLength ).Trim(), suffix ?? string.Empty );
		}

		
		public static string[] Split( this string self, char separator )
		{
			return self.Split( new[] { separator }, StringSplitOptions.None );
		}

		
		public static string[] Split( this string self, char separator, StringSplitOptions options )
		{
			return self.Split( new[] { separator }, options );
		}

		
		public static string[] Split( this string self, string separator )
		{
			return self.Split( new[] { separator }, StringSplitOptions.None );
		}

		
		public static string[] Split( this string self, string separator, StringSplitOptions options )
		{
			return self.Split( new[] { separator }, options );
		}

		
		public static string[] Split( this string self, params string[] separator )
		{
			return self.Split( separator, StringSplitOptions.None );
		}

		
		public static string ReplaceEmpty( this string self, string oldValue )
		{
			return self.Replace( oldValue, string.Empty );
		}

		
		public static string AddFront( this string self, string value )
		{
			return self.Insert( 0, value );
		}

		
		public static string ToTitleCase( this string self )
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase( self );
		}

		
		public static string SnakeToUpperCamel( this string self )
		{
			if ( string.IsNullOrEmpty( self ) ) return self;

			return self
				.Split( new[] { '_' }, StringSplitOptions.RemoveEmptyEntries )
				.Select( s => char.ToUpperInvariant( s[ 0 ] ) + s.Substring( 1, s.Length - 1 ) )
				.Aggregate( string.Empty, ( s1, s2 ) => s1 + s2 )
			;
		}

		
		public static string SnakeToLowerCamel( this string self )
		{
			if ( string.IsNullOrEmpty( self ) ) return self;

			return self.SnakeToUpperCamel().Insert( 0, char.ToLowerInvariant( self[ 0 ] ).ToString() ).Remove( 1, 1 );
		}

		
		public static string ToWindowsPath( this string self )
		{
			return self.Replace( "/", "\\" );
		}

		
		public static string ToMacPath( this string self )
		{
			return self.Replace( "\\", "/" );
		}

		
		public static int GetByteCount( this string self )
		{
			int count = 0;
			for ( int i = 0; i < self.Length; i++ )
			{
				if ( StringUtils.IsChar2Byte( self[ i ] ) )
				{
					count++;
				}
				count++;
			}
			return count;
		}

		
		public static string SubstringInByte( this string self, int byteCount )
		{
			string tmp = "";
			int count = 0;
			for ( int i = 0; i < self.Length; i++ )
			{
				char c = self[ i ];
				if ( StringUtils.IsChar2Byte( c ) )
				{
					count++;
				}
				count++;
				if ( count > byteCount )
				{
					break;
				}
				tmp += c;
			}
			return tmp;
		}

		
		public static string SafeSubstringInByte( this string self, int byteCount )
		{
			return byteCount < self.GetByteCount() ? self.SubstringInByte( byteCount ) : self;
		}

		
		public static string Trim( this string self, params string[] trimChars )
		{
			return self.Trim( trimChars.Select( c => Convert.ToChar( c ) ).ToArray() );
		}

		
		public static string TrimEnd( this string self, params string[] trimChars )
		{
			return self.TrimEnd( trimChars.Select( c => Convert.ToChar( c ) ).ToArray() );
		}

		
		public static string TrimStart( this string self, params string[] trimChars )
		{
			return self.TrimStart( trimChars.Select( c => Convert.ToChar( c ) ).ToArray() );
		}

		
		public static string Repeat( this string self, int repeatCount )
		{
			var builder = new StringBuilder();
			for ( int i = 0; i < repeatCount; i++ )
			{
				builder.Append( self );
			}
			return builder.ToString();
		}

		
		public static bool CompareIgnoreWidthAndCase( this string self, string value )
		{
			var compareInfo = CultureInfo.CurrentCulture.CompareInfo;
			return 0 <= compareInfo.IndexOf( self, value, CompareOptions.IgnoreWidth | CompareOptions.IgnoreCase );
		}

		
		public static bool IncludeAny( this string self, params string[] list )
		{
			return list.Any( c => self.Contains( c ) );
		}

		
		public static string SafeAddExtension( this string self, string extension )
		{
			if ( self.EndsWith( extension ) ) return self;
			return self + extension;
		}

		
		public static string RemoveNewLine( this string self )
		{
			return self.ReplaceEmpty( "\n" ).ReplaceEmpty( "\r" );
		}

		
		public static string Coloring( this string str, string color )
		{
			return string.Format( "<color={0}>{1}</color>", color, str );
		}

		
		public static string Red( this string str )
		{
			return str.Coloring( "red" );
		}

		
		public static string Green( this string str )
		{
			return str.Coloring( "green" );
		}

		
		public static string Blue( this string str )
		{
			return str.Coloring( "blue" );
		}

		
		public static string Resize( this string str, int size )
		{
			return string.Format( "<size={0}>{1}</size>", size, str );
		}

		
		public static string Small( this string str )
		{
			return str.Resize( 9 );
		}

		
		public static string Medium( this string str )
		{
			return str.Resize( 11 );
		}

		
		public static string Large( this string str )
		{
			return str.Resize( 16 );
		}

		
		public static string Bold( this string str )
		{
			return string.Format( "<b>{0}</b>", str );
		}

		
		public static string Italic( this string str )
		{
			return string.Format( "<i>{0}</i>", str );
		}

		public static int CountString( this string self, string str )
		{
			return ( self.Length - self.Replace( str, "" ).Length ) / str.Length;
		}
		
		public static string Reduce( this string self, string str, int remainCount )
		{
			while ( remainCount < self.CountString( str ) )
			{
				self = self.Remove( self.IndexOf( str ), str.Length );
			}
			return self;
		}

		
		public static string LastReduce( this string self, string str, int remainCount )
		{
			while ( remainCount < self.CountString( str ) )
			{
				self = self.Remove( self.LastIndexOf( str ), str.Length );
			}
			return self;
		}

		
		public static string ToShiftJis( this string unicodeStrings )
		{
#if UNITY_EDITOR
			var unicode = Encoding.Unicode;
			var unicodeByte = unicode.GetBytes( unicodeStrings );
			var s_jis = Encoding.GetEncoding( "shift_jis" );
			var s_jisByte = Encoding.Convert( unicode, s_jis, unicodeByte );
			var s_jisChars = new char[ s_jis.GetCharCount( s_jisByte, 0, s_jisByte.Length ) ];
			s_jis.GetChars( s_jisByte, 0, s_jisByte.Length, s_jisChars, 0 );
			return new string( s_jisChars );
#else
			return unicodeStrings;
#endif
		}

		
		public static string Unescape( this string self )
		{
			return Regex.Unescape( self );
		}

		
		public static string Escape( this string self )
		{
			return Regex.Escape( self );
		}

		
		public static string[] SubstringAtCount( this string self, int count )
		{
			var result = new List<string>();
			var length = ( int )Math.Ceiling( ( double )self.Length / count );

			for ( int i = 0; i < length; i++ )
			{
				int start = count * i;
				if ( self.Length <= start )
				{
					break;
				}
				if ( self.Length < start + count )
				{
					result.Add( self.Substring( start ) );
				}
				else
				{
					result.Add( self.Substring( start, count ) );
				}
			}

			return result.ToArray();
		}

		
		public static string RemoveAtLast( this string self, string value )
		{
			return self.Remove( self.LastIndexOf( value ), value.Length );
		}
		
		
		public static bool CustomStartsWith( this string a, string b )
		{
			int aLen = a.Length;
			int bLen = b.Length;
			int ap   = 0;
			int bp   = 0;

			while ( ap < aLen && bp < bLen && a[ ap ] == b[ bp ] )
			{
				ap++;
				bp++;
			}

			return ( bp == bLen && aLen >= bLen ) || ( ap == aLen && bLen >= aLen );
		}
		
		
		public static bool CustomEndsWith( this string a, string b )
		{
			int ap = a.Length - 1;
			int bp = b.Length - 1;

			while ( ap >= 0 && bp >= 0 && a[ ap ] == b[ bp ] )
			{
				ap--;
				bp--;
			}

			return ( bp < 0 && a.Length >= b.Length ) || ( ap < 0 && b.Length >= a.Length );
		}
		
		
		public static bool IsLower( this string self )
		{
			for ( int i = 0; i < self.Length; i++ )
			{
				if ( char.IsUpper( self[ i ] ) )
				{
					return false;
				}
			}

			return true;
		}
		
		
		public static bool IsUpper( this string self )
		{
			for ( int i = 0; i < self.Length; i++ )
			{
				if ( char.IsLower( self[ i ] ) )
				{
					return false;
				}
			}

			return true;
		}
		
		
		public static string GetFirstLine( this string self )
		{
			var separator = new [] { Environment.NewLine };

			return self
					.Split( separator, StringSplitOptions.None )
					.FirstOrDefault()
				;
		}
	}
}