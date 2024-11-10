using UnityEngine;

namespace GameFramework
{
	public static class SpriteRendererExt
	{
		public static void SetAlpha(this SpriteRenderer self, float alpha)
		{
			var color = self.color;
			color.a = alpha;
			self.color = color;
		}
	}
}