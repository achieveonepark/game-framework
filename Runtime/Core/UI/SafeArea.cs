using UnityEngine;

namespace GameFramework
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class UISafeArea : MonoBehaviour
    {
        [SerializeField] private bool _ignoreLeft = default;
        [SerializeField] private bool _ignoreRight = default;
        [SerializeField] private bool _ignoreTop = default;
        [SerializeField] private bool _ignoreBottom = default;

        private RectTransform _rectTransformCache;
        private Rect _currentArea;
        private bool _isForce;

        private RectTransform RectTransform
        {
            get
            {
                if (_rectTransformCache is null)
                {
                    TryGetComponent(out _rectTransformCache);
                }
                
                return _rectTransformCache;
            }
        }

        private void OnEnable()
        {
            Apply(true);
        }
        
        private void Apply(bool isForce = false)
        {
            var safeArea = Screen.safeArea;
            if (!isForce && _currentArea == safeArea) return;

            ApplyFrom(safeArea, isForce);
        }

        private void ApplyFrom(Rect area, bool isForce = false)
        {
            if (RectTransform == null) return;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (_ignoreLeft) anchorMin.x = 0f;
            if (_ignoreRight) anchorMax.x = 1f;
            if (_ignoreTop) anchorMax.y = 1f;
            if (_ignoreBottom) anchorMin.y = 0f;

            RectTransform.anchoredPosition = Vector2.zero;
            RectTransform.sizeDelta = Vector2.zero;
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;

            _currentArea = area;
        } 
        
// #if UNITY_EDITOR
//        private void Update()
//        {
//            if (Application.isPlaying) return;
//            Apply(_isForce);
//            _isForce = false;
//        }
//
//        private void OnValidate()
//        {
//            if (Application.isPlaying) return;
//            _isForce = true;//
//        }
// #endif
    }
}