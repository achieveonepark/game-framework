using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 리스트 팝업에서 사용하는 개별 아이템 UI의 기본 클래스입니다.
    /// ItemListPopup에서 반복적으로 생성되는 단위 UI 요소입니다.
    /// 예: 인벤토리의 각 아이템 슬롯, 상점의 각 상품 등이 이 클래스를 상속합니다.
    /// </summary>
    public class Item : MonoBehaviour
    {
        /// <summary>
        /// 이 아이템 UI를 새 데이터로 갱신합니다.
        /// 게임 오브젝트를 활성화하고, 자식 클래스에서 data를 사용해 내용을 채웁니다.
        /// </summary>
        /// <param name="data">아이템에 표시할 데이터 객체</param>
        public virtual void Refresh(object data)
        {
            // 비활성화된 아이템을 표시 상태로 만듭니다
            gameObject.SetActive(true);
        }
    }
}
