using UnityEngine;
using UnityEngine.UI;

namespace GameFramework.Sample
{
    /// <summary>
    /// 인벤토리 팝업 내 개별 아이템 슬롯입니다.
    /// Item을 상속해 Refresh(object data)로 ItemData를 받아 UI를 갱신합니다.
    /// </summary>
    public class ItemSlot : Item
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _attackText;
        [SerializeField] private Image _rarityBorder;

        private static readonly Color[] RarityColors =
        {
            Color.white,                          // Common
            new Color(0.4f, 0.6f, 1f),            // Rare
            new Color(0.6f, 0.2f, 1f),            // Epic
            new Color(1f, 0.8f, 0.1f),            // Legendary
        };

        public override void Refresh(object data)
        {
            base.Refresh(data);

            if (data is not ItemData item) return;

            _nameText.text = item.Name;
            _attackText.text = $"ATK {item.AttackPower}";
            _rarityBorder.color = RarityColors[(int)item.Rarity];
        }
    }
}
