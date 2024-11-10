using System;
using UnityEngine.Events;

namespace GameFramework
{

	public static class UnityEventExt
	{
		public static void AddListener(this UnityEvent self, Action call)
		{
			self.AddListener(() => call());
		}

		public static void SetListener(this UnityEvent self, Action call)
		{
			self.RemoveAllListeners();
			self.AddListener(() => call());
		}
	}
}