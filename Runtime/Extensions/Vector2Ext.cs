using UnityEngine;

namespace GameFramework
{

	public static class Vector2Ext
	{

		public static Vector2 Round(this Vector2 self)
		{
			return new Vector2
			(
				Mathf.Round(self.x),
				Mathf.Round(self.y)
			);
		}
	}
}