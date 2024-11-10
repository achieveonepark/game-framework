namespace GameFramework.Native
{
    public static class OSNative
    {
        public static long GetFreeSpace() => Storage.GetFreeSpace();
    }
}