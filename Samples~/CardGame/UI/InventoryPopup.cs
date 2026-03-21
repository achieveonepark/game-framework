using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework.Sample
{
    /// <summary>
    /// 인벤토리 팝업입니다.
    /// PopupBase를 상속하고 BaseUI의 컴포넌트 캐싱 시스템을 활용합니다.
    ///
    /// 네이밍 컨벤션:
    ///   Btn_Close   → Button  (prefix: Btn)
    ///   Txt_Title   → Text    (prefix: Txt)
    ///   Img_Panel   → Image   (prefix: Img)
    /// </summary>
    public class InventoryPopup : PopupBase
    {
        [SerializeField] private Transform _slotParent;
        [SerializeField] private ItemSlot _slotPrefab;

        private readonly List<ItemSlot> _slots = new List<ItemSlot>();

        public override void Caching()
        {
            // 코드에서 직접 컴포넌트를 추가 캐싱해야 할 때 여기에 작성합니다.
            // BaseUI의 Get<T>()로 꺼낸 버튼에 리스너를 붙이는 패턴입니다.
            Get<Button>("Close").onClick.AddListener(Close);
        }

        public override void Open()
        {
            base.Open();
            Render(Core.Get<PlayerManager>().GetContainer<InventoryContainer>());
        }

        public override void Open(object data)
        {
            base.Open();
            if (data is InventoryContainer container)
                Render(container);
        }

        public override void Refresh()
        {
            Render(Core.Get<PlayerManager>().GetContainer<InventoryContainer>());
        }

        private void Render(InventoryContainer container)
        {
            var items = container.GetAll();
            int index = 0;

            foreach (var kv in items)
            {
                if (index >= _slots.Count)
                {
                    var slot = Instantiate(_slotPrefab, _slotParent);
                    _slots.Add(slot);
                }

                _slots[index].Refresh(kv.Value);
                index++;
            }

            // 남은 슬롯 비활성화
            for (int i = index; i < _slots.Count; i++)
                _slots[i].gameObject.SetActive(false);

            Get<Text>("Title").text = $"인벤토리 ({index}개)";
        }
    }
}
