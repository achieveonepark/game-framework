using System;
using UnityEngine;

namespace GameFramework
{

	public static class IntExt
	{

		public static void Times(this int self, Action act)
		{
			for (int i = 0; i < self; i++)
			{
				act();
			}
		}


		public static void Times(this int self, Action<int> act)
		{
			for (int i = 0; i < self; i++)
			{
				act(i);
			}
		}


		public static void TimesReverse(this int self, Action<int> act)
		{
			for (int i = self - 1; 0 <= i; i--)
			{
				act(i);
			}
		}


		public static string ZeroFill(this int self, int numberOfDigits)
		{
			return self.ToString("D" + numberOfDigits.ToString());
		}


		public static string FixedPoint(this int self, int numberOfDigits)
		{
			return self.ToString("F" + numberOfDigits.ToString());
		}


		public static int Repeat(this int self, int value, int max)
		{
			if (max == 0) return self;
			return (self + value + max) % max;
		}


		public static bool IsEven(this int self)
		{
			return self % 2 == 0;
		}


		public static bool IsOdd(this int self)
		{
			return self % 2 == 1;
		}


		public static float Clamp(this int value, int min, int max)
		{
			return Mathf.Clamp(value, min, max);
		}
	}
}