using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 터치/클릭할 수 있는 2D 게임 오브젝트의 추상 기반 클래스입니다.
    /// ObjectTouchManager가 이 컴포넌트를 가진 오브젝트를 감지하여 OnTouched()를 호출합니다.
    /// 이 클래스를 상속하고 OnTouched()를 구현하면 터치 가능한 오브젝트를 만들 수 있습니다.
    /// </summary>
    public abstract class TouchableObject : MonoBehaviour
    {
        // 터치됐을 때 실행될 이벤트
        /// <summary>
        /// 이 오브젝트가 터치/클릭되었을 때 호출됩니다.
        /// 자식 클래스에서 반드시 구현해야 하는 추상 메서드입니다.
        /// 예: 캐릭터를 터치하면 애니메이션 재생, 아이템을 터치하면 수집 등의 로직을 작성합니다.
        /// </summary>
        public abstract void OnTouched();
    }
}
