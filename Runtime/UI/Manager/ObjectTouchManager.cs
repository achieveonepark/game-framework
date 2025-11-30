using UnityEngine;

namespace GameFramework
{
    public class ObjectTouchManager : MonoSingleton<ObjectTouchManager>
    {
        private Camera mainCamera;

        public override void InitializeSingleton()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            HandleTouch();
        }

        private void HandleTouch()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (!hit.collider)
            {
                return;
            }

            var obj = hit.collider.gameObject;

            if (obj.TryGetComponent<TouchableObject>(out var touchableObject) is false)
            {
                return;
            }

            touchableObject.OnTouched();
        }
    }
}