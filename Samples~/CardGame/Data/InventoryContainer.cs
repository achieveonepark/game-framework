using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Sample
{
    /// <summary>
    /// 플레이어 인벤토리 데이터를 관리합니다.
    /// PlayerDataContainerBase&lt;int, ItemData&gt; 상속으로 Dictionary 기반 저장/조회가 자동 제공됩니다.
    /// </summary>
    public class InventoryContainer : PlayerDataContainerBase<int, ItemData>
    {
        public InventoryContainer()
        {
            DataKey = nameof(InventoryContainer);
        }

        public void AddItem(ItemData item) => Add(item.Id, item);

        public ItemData GetItem(int id) => GetInfo(id);

        public bool RemoveItem(int id) => RemoveInfo(id);

        public IEnumerable<ItemData> GetByRarity(ItemRarity rarity)
            => GetAll().Where(kv => kv.Value.Rarity == rarity).Select(kv => kv.Value);

        public int TotalAttackPower()
            => GetAll().Sum(kv => kv.Value.AttackPower);
    }
}
