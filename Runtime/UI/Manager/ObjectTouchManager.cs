using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 씬에 있는 TouchableObject를 감지하고 터치/클릭 이벤트를 전달하는 매니저입니다.
    /// MonoSingleton을 상속하므로 씬 전체에서 하나의 인스턴스만 존재합니다.
    /// 매 프레임 마우스 클릭(또는 터치)을 감지하고, 2D Raycast로 충돌한 오브젝트를 찾아
    /// TouchableObject 컴포넌트가 있으면 OnTouched()를 호출합니다.
    /// </summary>
    public class ObjectTouchManager : MonoSingleton<ObjectTouchManager>
    {
        // 스크린 좌표를 월드 좌표로 변환하기 위해 메인 카메라를 저장합니다
        private Camera mainCamera;

        /// <summary>
        /// 싱글톤이 초기화될 때 호출됩니다.
        /// 메인 카메라를 캐싱합니다.
        /// </summary>
        public override void InitializeSingleton()
        {
            mainCamera = Camera.main;
        }

        /// <summary>
        /// Unity의 Update는 매 프레임 호출됩니다.
        /// 터치/클릭 입력을 체크합니다.
        /// </summary>
        void Update()
        {
            HandleTouch();
        }

        /// <summary>
        /// 마우스 클릭(0번 버튼 = 왼쪽 클릭 or 터치)을 감지하고,
        /// 클릭 위치에 있는 2D 오브젝트를 찾아 TouchableObject라면 이벤트를 발생시킵니다.
        /// </summary>
        private void HandleTouch()
        {
            // GetMouseButtonDown(0)은 왼쪽 마우스 버튼이나 첫 번째 터치가 시작된 프레임에 true를 반환합니다
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            // 마우스 스크린 좌표를 2D 월드 좌표로 변환합니다
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 해당 위치에 있는 2D Collider를 Raycast로 감지합니다
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // 충돌한 Collider가 없으면 아무것도 하지 않습니다
            if (!hit.collider)
            {
                return;
            }

            var obj = hit.collider.gameObject;

            // 충돌한 오브젝트에 TouchableObject 컴포넌트가 없으면 무시합니다
            if (obj.TryGetComponent<TouchableObject>(out var touchableObject) is false)
            {
                return;
            }

            // TouchableObject의 터치 이벤트를 발생시킵니다
            touchableObject.OnTouched();
        }
    }
}
