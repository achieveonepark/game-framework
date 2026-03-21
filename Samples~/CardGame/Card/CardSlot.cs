using UnityEngine;

namespace GameFramework.Sample
{
    /// <summary>
    /// 카드를 놓을 수 있는 슬롯입니다.
    /// Draggable의 OnTouchUp에서 Physics2D.OverlapCircleAll로 감지됩니다.
    /// Collider2D가 반드시 붙어 있어야 합니다.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class CardSlot : MonoBehaviour
    {
        public bool IsEmpty => _placedCard == null;

        private CardDraggable _placedCard;

        public void Place(CardDraggable card)
        {
            _placedCard = card;
        }

        public CardDraggable Take()
        {
            var card = _placedCard;
            _placedCard = null;
            return card;
        }
    }
}
