using UnityEngine;

namespace GameFramework
{

	public static class RectTransformExt
	{

		public static float GetWidth(this RectTransform self)
		{
			return self.sizeDelta.x;
		}


		public static float GetHeight(this RectTransform self)
		{
			return self.sizeDelta.y;
		}


		public static void SetWidth(this RectTransform self, float width)
		{
			self.sizeDelta = new Vector2(width, self.sizeDelta.y);
		}


		public static void SetHeight(this RectTransform self, float height)
		{
			self.sizeDelta = new Vector2(self.sizeDelta.x, height);
		}


		public static void SetSizeX(this RectTransform self, float width)
		{
			self.sizeDelta = new Vector2(width, self.sizeDelta.y);
		}


		public static void SetSizeY(this RectTransform self, float height)
		{
			self.sizeDelta = new Vector2(self.sizeDelta.x, height);
		}


		public static void SetSize(this RectTransform self, float width, float height)
		{
			self.sizeDelta = new Vector2(width, height);
		}


		public static void SetSize(this RectTransform self, Vector2 sizeDelta)
		{
			self.sizeDelta = sizeDelta;
		}


		public static void SetAnchoredPosition(this RectTransform self, Vector2 value)
		{
			self.anchoredPosition = value;
		}


		public static void SetAnchoredPosition(this RectTransform self, float x, float y)
		{
			self.anchoredPosition = new Vector2(x, y);
		}


		public static void SetAnchoredPositionX(this RectTransform self, float x)
		{
			var pos = self.anchoredPosition;
			pos.x = x;
			self.anchoredPosition = pos;
		}


		public static void SetAnchoredPositionY(this RectTransform self, float y)
		{
			var pos = self.anchoredPosition;
			pos.y = y;
			self.anchoredPosition = pos;
		}


		public static void SetPivotX(this RectTransform self, float x)
		{
			var size = self.pivot;
			size.x = x;
			self.pivot = size;
		}


		public static void SetPivotY(this RectTransform self, float y)
		{
			var size = self.pivot;
			size.y = y;
			self.pivot = size;
		}


		public static void SetPivot(this RectTransform self, Vector2 pivot)
		{
			self.pivot = pivot;
		}


		public static void SetPivot(this RectTransform self, float x, float y)
		{
			self.pivot = new Vector2(x, y);
		}


		public static void SetOffsetMinX(this RectTransform self, float x)
		{
			var offsetMin = self.offsetMin;
			offsetMin.x = x;
			self.offsetMin = offsetMin;
		}


		public static void SetOffsetMinY(this RectTransform self, float y)
		{
			var offsetMin = self.offsetMin;
			offsetMin.y = y;
			self.offsetMin = offsetMin;
		}


		public static void SetOffsetMin(this RectTransform self, Vector2 offsetMin)
		{
			self.offsetMin = offsetMin;
		}


		public static void SetOffsetMin(this RectTransform self, float x, float y)
		{
			self.offsetMin = new Vector2(x, y);
		}


		public static void SetOffsetMaxX(this RectTransform self, float x)
		{
			var offsetMax = self.offsetMax;
			offsetMax.x = x;
			self.offsetMax = offsetMax;
		}


		public static void SetOffsetMaxY(this RectTransform self, float y)
		{
			var offsetMax = self.offsetMax;
			offsetMax.y = y;
			self.offsetMax = offsetMax;
		}


		public static void SetOffsetMax(this RectTransform self, Vector2 offsetMax)
		{
			self.offsetMax = offsetMax;
		}


		public static void SetOffsetMax(this RectTransform self, float x, float y)
		{
			self.offsetMax = new Vector2(x, y);
		}
	}
}