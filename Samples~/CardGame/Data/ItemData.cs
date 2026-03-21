using System;
using GameFramework.Data;

namespace GameFramework.Sample
{
    public enum ItemRarity { Common, Rare, Epic, Legendary }

    [Serializable]
    public class ItemData : PlayerDataBase
    {
        public string Name;
        public int AttackPower;
        public int Cost;
        public ItemRarity Rarity;
        public string IconPath;
    }
}
