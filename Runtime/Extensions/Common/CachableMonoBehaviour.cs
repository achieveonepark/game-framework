using UnityEngine;

namespace GameFramework
{
	public abstract class CachableMonoBehaviour : MonoBehaviour
	{
		private Transform _transformCache;
		private RectTransform _rectTransformCache;

		public new Transform transform
		{
			get
			{
				if (_transformCache == null)
				{
					_transformCache = GetComponent<Transform>();
				}

				return _transformCache;
			}
		}

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