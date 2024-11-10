using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{

	public static class GraphicExt
	{
		//------------------------------------------------------------------------------
		// RectTransformExt
		//------------------------------------------------------------------------------

		public static float GetWidth(this Graphic self)
		{
			return self.rectTransform.GetWidth();
		}


		public static float GetHeight(this Graphic self)
		{
			return self.rectTransform.GetHeight();
		}


		public static void SetWidth(this Graphic self, float width)
		{
			self.rectTransform.SetWidth(width);
		}


		public static void SetHeight(this Graphic self, float height)
		{
			self.rectTransform.SetHeight(height);
		}


		public static void SetSizeX(this Graphic self, float width)
		{
			self.rectTransform.SetSizeX(width);
		}


		public static void SetSizeY(this Graphic self, float height)
		{
			self.rectTransform.SetSizeY(height);
		}


		public static void SetSize(this Graphic self, float width, float height)
		{
			self.rectTransform.SetSize(width, height);
		}


		public static void SetSize(this Graphic self, Vector2 sizeDelta)
		{
			self.rectTransform.SetSize(sizeDelta);
		}


		public static void SetAnchoredPosition(this Graphic self, Vector2 value)
		{
			self.rectTransform.SetAnchoredPosition(value);
		}


		public static void SetAnchoredPosition(this Graphic self, float x, float y)
		{
			self.rectTransform.SetAnchoredPosition(x, y);
		}


		public static void SetAnchoredPositionX(this Graphic self, float x)
		{
			self.rectTransform.SetAnchoredPositionX(x);
		}


		public static void SetAnchoredPositionY(this Graphic self, float y)
		{
			self.rectTransform.SetAnchoredPositionY(y);
		}


		public static void SetPivotX(this Graphic self, float x)
		{
			self.rectTransform.SetPivotX(x);
		}


		public static void SetPivotY(this Graphic self, float y)
		{
			self.rectTransform.SetPivotY(y);
		}


		public static void SetPivot(this Graphic self, Vector2 pivot)
		{
			self.rectTransform.SetPivot(pivot);
		}


		public static void SetPivot(this Graphic self, float x, float y)
		{
			self.rectTransform.SetPivot(x, y);
		}


		public static void SetOffsetMinX(this Graphic self, float x)
		{
			self.rectTransform.SetOffsetMinX(x);
		}


		public static void SetOffsetMinY(this Graphic self, float y)
		{
			self.rectTransform.SetOffsetMinY(y);
		}


		public static void SetOffsetMin(this Graphic self, Vector2 offsetMin)
		{
			self.rectTransform.SetOffsetMin(offsetMin);
		}


		public static void SetOffsetMin(this Graphic self, float x, float y)
		{
			self.rectTransform.SetOffsetMin(x, y);
		}


		public static void SetOffsetMaxX(this Graphic self, float x)
		{
			self.rectTransform.SetOffsetMaxX(x);
		}


		public static void SetOffsetMaxY(this Graphic self, float y)
		{
			self.rectTransform.SetOffsetMaxY(y);
		}


		public static void SetOffsetMax(this Graphic self, Vector2 offsetMax)
		{
			self.rectTransform.SetOffsetMax(offsetMax);
		}


		public static void SetOffsetMax(this Graphic self, float x, float y)
		{
			self.rectTransform.SetOffsetMax(x, y);
		}

		//------------------------------------------------------------------------------
		// RectTransform
		//------------------------------------------------------------------------------
		public static Vector2 GetOffsetMax(this Graphic self)
		{
			return self.rectTransform.offsetMax;
		}

		public static Vector2 GetOffsetMin(this Graphic self)
		{
			return self.rectTransform.offsetMin;
		}

		public static void ForceUpdateRectTransforms(this Graphic self)
		{
			self.rectTransform.ForceUpdateRectTransforms();
		}

		public static void GetLocalCorners(this Graphic self, Vector3[] fourCornersArray)
		{
			self.rectTransform.GetLocalCorners(fourCornersArray);
		}

		public static void GetWorldCorners(this Graphic self, Vector3[] fourCornersArray)
		{
			self.rectTransform.GetWorldCorners(fourCornersArray);
		}

		//------------------------------------------------------------------------------
		// Original
		//------------------------------------------------------------------------------

		public static void SetAlpha(this Graphic self, float alpha)
		{
			var color = self.color;
			color.a = alpha;
			self.color = color;
		}

		//------------------------------------------------------------------------------
		// ContentSizeFitter
		//------------------------------------------------------------------------------
		public static ContentSizeFitter.FitMode GetHorizontalFit(this Graphic self)
		{
			return self.GetComponent<ContentSizeFitter>().horizontalFit;
		}

		public static void SetHorizontalFit(this Graphic self, ContentSizeFitter.FitMode value)
		{
			self.GetComponent<ContentSizeFitter>().horizontalFit = value;
		}

		public static ContentSizeFitter.FitMode GetVerticalFit(this Graphic self)
		{
			return self.GetComponent<ContentSizeFitter>().verticalFit;
		}

		public static void SetVerticalFit(this Graphic self, ContentSizeFitter.FitMode value)
		{
			self.GetComponent<ContentSizeFitter>().verticalFit = value;
		}

		public static void SetLayoutHorizontal(this Graphic self)
		{
			self.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
		}

		public static void SetLayoutVertical(this Graphic self)
		{
			self.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		}
	}
}