using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{

	public static class ImageExt
	{

		public static void SetSpriteAndSnap(this Image self, Sprite sprite)
		{
			self.sprite = sprite;
			self.SetNativeSize();
		}
	}
}