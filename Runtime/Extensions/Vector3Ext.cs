using UnityEngine;

namespace GameFramework
{

	public static class Vector3Ext
	{

		public static bool IsUniform(this Vector3 self)
		{
			return Mathf.Approximately(self.x, self.y) && Mathf.Approximately(self.x, self.z);
		}


		public static Vector3 Round(this Vector3 self)
		{
			return new Vector3
			(
				Mathf.Round(self.x),
				Mathf.Round(self.y),
				Mathf.Round(self.z)
			);
		}
	}
}