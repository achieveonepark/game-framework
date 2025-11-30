using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    /// <summary>
    /// 드래그 할 수 있는 기능을 제공합니다.
    /// </summary>
    public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// 드래그 후 터치를 뗄 때 호출됩니다. 위치해있는 Collider들을 반환합니다. 
        /// </summary>
        public event Action<Collider2D[]> OnTouchUp;
        
        /// <summary>
        /// 터치 중인 시점에 계속 호출됩니다. 이동 중인 현재 Pos를 반환합니다.
        /// </summary>
        public event Action<Vector3> OnTouching;
        
        /// <summary>
        /// 드래그를 시작하기 위한 터치 입력 시 호출됩니다.
        /// </summary>
        public event Action OnTouchDown;

        /// <summary>
        /// OnTouchUp 시점에서 이 Pos를 통해 기존 시점으로 돌아갈 지 판단합니다.
        /// </summary>
        protected Vector3 originalPos;
        
        private bool _isDragging;
        private Camera _mainCamera;
            
        protected virtual void Start()
        {
            _mainCamera = Camera.main;
            
            if(_mainCamera.TryGetComponent<Physics2DRaycaster>(out var raycaster) is false)
            {
                _mainCamera.AddComponent<Physics2DRaycaster>();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging is false)
            {
                return;
            }
        
            var newPos = _mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, _mainCamera.nearClipPlane));
            newPos.z = 0;
            transform.position = newPos;
            OnTouching(newPos);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _isDragging = true;
                originalPos = transform.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            OnTouchUp(Physics2D.OverlapCircleAll(transform.position, 0.5f));
        }
    }
}