using UnityEngine;
using UnityEngine.U2D;

namespace GameFramework
{
	public static class SpriteAtlasExt
	{
		public static Sprite[] GetSprites(this SpriteAtlas self)
		{
			var sprites = new Sprite[self.spriteCount];
			self.GetSprites(sprites);
			return sprites;
		}
	}
}