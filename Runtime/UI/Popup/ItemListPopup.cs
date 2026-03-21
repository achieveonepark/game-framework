using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 여러 개의 아이템(Item)을 리스트 형태로 표시하는 팝업의 기반 클래스입니다.
    /// 제네릭 타입 T를 사용하여 다양한 종류의 Item을 표시할 수 있습니다.
    /// 예: ItemListPopup&lt;InventoryItem&gt; 처럼 특정 아이템 타입에 특화된 리스트 팝업을 만들 수 있습니다.
    /// </summary>
    /// <typeparam name="T">리스트에 표시할 아이템 타입. Item 클래스를 상속해야 합니다.</typeparam>
    public class ItemListPopup<T> : MonoBehaviour where T : Item
    {
        /// <summary>
        /// 리스트에 생성할 아이템의 프리팹입니다.
        /// Inspector에서 직접 연결해야 합니다.
        /// </summary>
        public T createdPrefab;

        /// <summary>
        /// 현재 화면에 표시 중인 아이템 인스턴스들의 목록입니다.
        /// 자식 클래스에서 이 리스트를 통해 아이템을 관리합니다.
        /// </summary>
        protected List<T> _list;
    }
}
