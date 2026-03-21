using UnityEngine;

namespace GameFramework
{
	/// <summary>
	/// Transform과 RectTransform을 캐싱하여 성능을 최적화하는 MonoBehaviour 기반 클래스입니다.
	/// 기본 MonoBehaviour의 transform 프로퍼티는 내부적으로 매번 GetComponent를 호출하는데,
	/// 이를 캐싱하면 반복 접근 시 성능이 향상됩니다.
	/// UI 요소나 자주 transform을 사용하는 컴포넌트에 적합합니다.
	/// </summary>
	public abstract class CachableMonoBehaviour : MonoBehaviour
	{
		// Transform 컴포넌트를 캐싱합니다. null이면 처음 접근 시 자동으로 가져옵니다.
		private Transform _transformCache;
		// RectTransform 컴포넌트를 캐싱합니다 (UI 오브젝트에만 존재합니다)
		private RectTransform _rectTransformCache;

		/// <summary>
		/// 캐싱된 Transform을 반환합니다.
		/// 기본 MonoBehaviour.transform을 new 키워드로 숨겨서 캐싱 버전을 사용하게 합니다.
		/// 처음 접근 시 GetComponent로 가져오고, 이후에는 캐싱된 값을 반환합니다.
		/// </summary>
		public new Transform transform
		{
			get
			{
				// null인 경우에만 GetComponent를 호출하여 비용을 절감합니다
				if (_transformCache == null)
				{
					_transformCache = GetComponent<Transform>();
				}

				return _transformCache;
			}
		}

		/// <summary>
		/// 캐싱된 RectTransform을 반환합니다.
		/// RectTransform은 Canvas 아래의 UI 오브젝트에만 존재합니다.
		/// 처음 접근 시 GetComponent로 가져오고, 이후에는 캐싱된 값을 반환합니다.
		/// </summary>
		public RectTransform rectTransform
		{
			get
			{
				if (_rectTransformCache == null)
				{
					_rectTransformCache = GetComponent<RectTransform>();
				}

				return _rectTransformCache;
			}
		}
	}
}
