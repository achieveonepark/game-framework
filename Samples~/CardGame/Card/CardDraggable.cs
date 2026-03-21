using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework.Sample
{
    /// <summary>
    /// 드래그 가능한 카드입니다.
    /// Draggable을 상속하여 드래그/드롭 로직에 카드 전용 동작을 추가합니다.
    /// </summary>
    public class CardDraggable : Draggable
    {
        [SerializeField] private float _snapRadius = 0.5f;

        private ItemData _itemData;
        private bool _isPlaced;

        public void Setup(ItemData data)
        {
            _itemData = data;

            OnTouchDown += HandleTouchDown;
            OnTouching += HandleTouching;
            OnTouchUp += HandleTouchUp;
        }

        private void OnDestroy()
        {
            OnTouchDown -= HandleTouchDown;
            OnTouching -= HandleTouching;
            OnTouchUp -= HandleTouchUp;
        }

        private void HandleTouchDown()
        {
            // 이미 슬롯에 놓인 카드는 드래그 불가
            if (_isPlaced)
            {
                // 필요 시 슬롯에서 회수 로직 추가
            }
        }

        private void HandleTouching(Vector3 worldPos)
        {
            // 드래그 중 - 카드 살짝 회전으로 생동감 표현
            float tilt = Mathf.Sin(Time.time * 10f) * 2f;
            transform.rotation = Quaternion.Euler(0f, 0f, tilt);
        }

        private void HandleTouchUp(Collider2D[] overlaps)
        {
            transform.rotation = Quaternion.identity;

            CardSlot targetSlot = null;
            foreach (var col in overlaps)
            {
                if (col.TryGetComponent<CardSlot>(out var slot) && slot.IsEmpty)
                {
                    targetSlot = slot;
                    break;
                }
            }

            if (targetSlot != null)
            {
                PlaceOnSlot(targetSlot);
            }
            // 슬롯 없으면 Draggable이 originalPos로 자동 복귀
        }

        private void PlaceOnSlot(CardSlot slot)
        {
            _isPlaced = true;
            slot.Place(this);
            transform.position = slot.transform.position;
            transform.rotation = Quaternion.identity;
        }
    }
}
