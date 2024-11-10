namespace GameFramework.Native
{
	internal static class Storage
	{
		private static readonly IStorage _storage =
#if !UNITY_EDITOR && UNITY_ANDROID
			new AndroidStorage();
#elif !UNITY_EDITOR && UNITY_IOS
			new iOSStorage();
#else
			new DefaultStorage();
#endif
		
		public static long GetFreeSpace()
		{
			return _storage.GetFreeSpace();
		}
	}
}