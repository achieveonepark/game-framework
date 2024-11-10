using UnityEngine;

namespace GameFramework
{
	public static class CameraUtils
	{
		public static Vector3 WorldToScreenSpaceOverlay
		(
			Camera worldCamera,
			Vector3 worldPosition
		)
		{
			return RectTransformUtility.WorldToScreenPoint
			(
				cam: worldCamera,
				worldPoint: worldPosition
			);
		}

		public static Vector3 WorldToScreenSpaceCamera
		(
			Camera worldCamera,
			Camera canvasCamera,
			RectTransform canvasRectTransform,
			Vector3 worldPosition
		)
		{
			var screenPoint = RectTransformUtility.WorldToScreenPoint
			(
				cam: worldCamera,
				worldPoint: worldPosition
			);

			RectTransformUtility.ScreenPointToLocalPointInRectangle
			(
				rect: canvasRectTransform,
				screenPoint: screenPoint,
				cam: canvasCamera,
				localPoint: out var localPoint
			);

			return localPoint;
		}
	}
}