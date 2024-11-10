using UnityEngine;

namespace GameFramework
{
	public static class RectExt
	{
		public static Rect Shift(this Rect self, Vector2 offset)
		{
			self.x += offset.x;
			self.y += offset.y;
			return self;
		}


		public static Rect Shift(this Rect self, float offsetX, float offsetY)
		{
			self.x += offsetX;
			self.y += offsetY;
			return self;
		}
	}
}