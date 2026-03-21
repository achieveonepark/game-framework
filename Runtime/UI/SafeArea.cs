using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 모바일 기기의 안전 영역(Safe Area)에 맞게 UI를 자동으로 조정하는 컴포넌트입니다.
    /// 안전 영역이란 노치(notch), 홈 바, 상태바 등으로 가려지지 않는 화면 영역입니다.
    /// 이 컴포넌트를 UI 패널에 붙이면 모든 기기에서 UI가 안전 영역 안에만 표시됩니다.
    /// [ExecuteAlways]이므로 에디터에서도 실행됩니다.
    /// [RequireComponent(RectTransform)]이므로 RectTransform이 반드시 있어야 합니다.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class UISafeArea : MonoBehaviour
    {
        // 안전 영역 적용 시 무시할 방향을 지정하는 필드들
        // true로 설정하면 해당 방향의 안전 영역 제한을 무시하고 화면 끝까지 확장합니다

        [SerializeField] private bool _ignoreLeft = default;
        [SerializeField] private bool _ignoreRight = default;
        [SerializeField] private bool _ignoreTop = default;
        [SerializeField] private bool _ignoreBottom = default;

        // RectTransform을 매번 GetComponent로 찾지 않도록 캐싱합니다
        private RectTransform _rectTransformCache;
        // 마지막으로 적용한 안전 영역 크기를 저장합니다 (변경 감지용)
        private Rect _currentArea;
        private bool _isForce;

        // RectTransform을 캐싱하여 성능을 최적화합니다
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

        /// <summary>
        /// 컴포넌트가 활성화될 때마다 호출됩니다. 강제로 안전 영역을 다시 적용합니다.
        /// </summary>
        private void OnEnable()
        {
            Apply(true);
        }

        /// <summary>
        /// 안전 영역을 적용합니다.
        /// 안전 영역이 변경되지 않았고 강제 적용이 아니면 건너뜁니다.
        /// </summary>
        /// <param name="isForce">true이면 안전 영역이 바뀌지 않아도 강제로 다시 적용합니다.</param>
        private void Apply(bool isForce = false)
        {
            var safeArea = Screen.safeArea;
            // 안전 영역이 이전과 같고 강제 적용이 아니면 불필요한 재계산을 건너뜁니다
            if (!isForce && _currentArea == safeArea) return;

            ApplyFrom(safeArea, isForce);
        }

        /// <summary>
        /// 주어진 Rect 값을 기반으로 RectTransform의 앵커(Anchor)를 계산하여 안전 영역을 적용합니다.
        /// 앵커를 0~1 정규화 좌표로 설정하여 해상도와 무관하게 동작합니다.
        /// </summary>
        /// <param name="area">적용할 안전 영역 Rect (픽셀 단위)</param>
        /// <param name="isForce">강제 적용 여부</param>
        private void ApplyFrom(Rect area, bool isForce = false)
        {
            if (RectTransform == null) return;

            // 안전 영역의 픽셀 좌표를 0~1 범위의 정규화 좌표로 변환합니다
            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            // 화면 해상도로 나누어 0~1 범위로 정규화합니다
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            // ignore 옵션이 켜진 방향은 안전 영역을 무시하고 화면 끝까지 확장합니다
            if (_ignoreLeft) anchorMin.x = 0f;
            if (_ignoreRight) anchorMax.x = 1f;
            if (_ignoreTop) anchorMax.y = 1f;
            if (_ignoreBottom) anchorMin.y = 0f;

            // RectTransform에 계산된 앵커 값을 적용합니다
            RectTransform.anchoredPosition = Vector2.zero;
            RectTransform.sizeDelta = Vector2.zero;
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;

            // 적용된 안전 영역을 저장해 다음 프레임 중복 계산을 방지합니다
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
