namespace GameFramework
{
    public static class ContainerManager
    {
        public static TInfo Get<TKey, TInfos, TInfo>(TKey id) 
            where TInfos : PlayerDataContainer<TKey, TInfo> 
            where TInfo : PlayerDataBase
        {
            return Player.GetInfos<TInfos>()?.GetInfo(id);
        }
    }
}