namespace GameFramework.Native
{
	internal sealed class DefaultStorage : IStorage
	{
		public long GetFreeSpace()
		{
			return -1;
		}
	}
}