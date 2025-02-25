﻿#if !UNITY_EDITOR && UNITY_IOS

using System.Runtime.InteropServices;

namespace GameFramework.Native
{
	internal sealed class iOSStorage : IStorage
	{
		//================================================================================
		// 関数(extern)
		//================================================================================
		[DllImport( "__Internal" )]
		private static extern long _GetFreeSpace();

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 空き容量を返します
		/// </summary>
		public long GetFreeSpace()
		{
			return _GetFreeSpace();
		}
	}
}

#endif