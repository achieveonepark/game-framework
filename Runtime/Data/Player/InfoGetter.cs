namespace GameFramework.Data.Player
{
    public static class InfoGetter
    {
        public static TInfo GetInfo<TKey, TInfos, TInfo>(TKey id) 
            where TInfos : InfosContainer<TKey, TInfo> 
            where TInfo : PlayerInfoBase
        {
            return Player.GetInfos<TInfos>()?.GetInfo(id);
        }
    }
}