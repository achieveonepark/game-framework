using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    /// <summary>
    /// 게임 오브젝트를 드래그할 수 있게 해주는 컴포넌트입니다.
    /// IDragHandler, IPointerDownHandler, IPointerUpHandler 인터페이스를 구현하여
    /// Unity의 이벤트 시스템과 연동됩니다.
    /// 드래그 후 손을 떼면 원래 위치로 돌아오며, 놓인 위치의 Collider 정보를 이벤트로 전달합니다.
    /// </summary>
    public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// 드래그 후 터치를 뗄 때 호출됩니다. 위치해있는 Collider들을 반환합니다.
        /// 이 이벤트를 구독하면 어떤 오브젝트 위에 드래그를 놓았는지 알 수 있습니다.
        /// </summary>
        public event Action<Collider2D[]> OnTouchUp;

        /// <summary>
        /// 터치 중인 시점에 계속 호출됩니다. 이동 중인 현재 Pos를 반환합니다.
        /// 드래그하는 동안 매 프레임 현재 위치를 알 수 있습니다.
        /// </summary>
        public event Action<Vector3> OnTouching;

        /// <summary>
        /// 드래그를 시작하기 위한 터치 입력 시 호출됩니다.
        /// 드래그 시작 시점에 필요한 초기화 작업을 수행할 수 있습니다.
        /// </summary>
        public event Action OnTouchDown;

        /// <summary>
        /// OnTouchUp 시점에서 이 Pos를 통해 기존 시점으로 돌아갈 지 판단합니다.
        /// 드래그 시작 전의 원래 위치를 저장합니다.
        /// </summary>
        protected Vector3 originalPos;

        // 현재 드래그 중인지 여부를 추적합니다
        private bool _isDragging;
        // 스크린 좌표를 월드 좌표로 변환하기 위해 메인 카메라를 캐싱합니다
        private Camera _mainCamera;

        /// <summary>
        /// Start는 Unity에서 오브젝트가 활성화된 후 첫 프레임에 한 번 호출됩니다.
        /// 드래그 감지에 필요한 Physics2DRaycaster를 카메라에 추가합니다.
        /// Physics2DRaycaster가 없으면 2D 오브젝트에 대한 포인터 이벤트를 받을 수 없습니다.
        /// </summary>
        protected virtual void Start()
        {
            _mainCamera = Camera.main;

            // 메인 카메라에 Physics2DRaycaster가 없으면 자동으로 추가합니다
            // 이 컴포넌트가 없으면 2D 물리 오브젝트에서 드래그 이벤트를 받을 수 없습니다
            if(_mainCamera.TryGetComponent<Physics2DRaycaster>(out var raycaster) is false)
            {
                _mainCamera.AddComponent<Physics2DRaycaster>();
            }
        }

        /// <summary>
        /// 드래그 중에 매 프레임 호출됩니다 (IDragHandler 구현).
        /// 마우스/터치 위치를 월드 좌표로 변환하여 오브젝트를 이동시킵니다.
        /// </summary>
        /// <param name="eventData">포인터 위치 등 입력 정보를 담은 이벤트 데이터</param>
        public void OnDrag(PointerEventData eventData)
        {
            // OnPointerDown에서 드래그 시작이 확인된 경우에만 이동합니다
            if (_isDragging is false)
            {
                return;
            }

            // 화면(스크린) 좌표를 3D 월드 좌표로 변환합니다. nearClipPlane을 Z로 사용하면 카메라에서 가장 가까운 위치가 됩니다.
            var newPos = _mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, _mainCamera.nearClipPlane));
            // 2D 게임이므로 Z축은 0으로 고정합니다
            newPos.z = 0;
            transform.position = newPos;
            // 현재 위치를 구독자에게 전달합니다
            OnTouching?.Invoke(newPos);
        }

        /// <summary>
        /// 화면을 눌렀을 때 호출됩니다 (IPointerDownHandler 구현).
        /// 왼쪽 버튼(또는 터치)으로 누른 경우에만 드래그를 시작합니다.
        /// </summary>
        /// <param name="eventData">어떤 버튼이 눌렸는지 등 입력 정보</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _isDragging = true;
                // 나중에 원래 위치로 돌아오기 위해 현재 위치를 저장합니다
                originalPos = transform.position;

                OnTouchDown?.Invoke();
            }
        }

        /// <summary>
        /// 화면에서 손/마우스를 뗄 때 호출됩니다 (IPointerUpHandler 구현).
        /// 드래그를 종료하고 원래 위치로 돌아간 후, 놓인 위치의 Collider 목록을 이벤트로 전달합니다.
        /// </summary>
        /// <param name="eventData">포인터 입력 이벤트 데이터</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;

            // 오브젝트를 드래그 시작 전의 원래 위치로 되돌립니다
            transform.position = originalPos;
            // 원래 위치 반경 0.5f 내의 모든 2D Collider를 찾아 이벤트로 전달합니다
            OnTouchUp?.Invoke(Physics2D.OverlapCircleAll(transform.position, 0.5f));
        }
    }
}
