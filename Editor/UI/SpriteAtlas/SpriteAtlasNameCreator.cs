using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameFramework.Editor.UI
{
	public class SpriteAtlasNameCreator
	{
		private sealed class FormatData
		{
			private readonly string _format;
			private readonly string _name;
			private readonly string _argument;
			private readonly string _arg0;

			public string Format
			{
				get { return _format; }
			}

			public string Name
			{
				get { return _name; }
			}

			public string Argument
			{
				get { return _argument; }
			}

			public string Arg0
			{
				get { return _arg0; }
			}

			public FormatData(int count, string name, string label)
			{
				_format = name;

				_name = label
						.Replace("/", "_")
						.Trim('_')
						.Replace("__", "_")
					;

				var range = Enumerable.Range(0, count);

				_argument = string.Join(", ", range.Select(c => "object arg" + c).ToArray());
				_arg0 = string.Join(", ", range.Select(c => "arg" + c).ToArray());
			}
		}

		private sealed class FormatDataComparer : IEqualityComparer<FormatData>
		{
			public bool Equals(FormatData x, FormatData y)
			{
				return x.Name == y.Name;
			}

			public int GetHashCode(FormatData obj)
			{
				return obj.Name.GetHashCode();
			}
		}

		private static readonly string[] INVALID_CHARS =
		{
			" ", "!", "\"", "#", "$",
			"%", "&", "\'", "(", ")",
			"-", "=", "^", "~", "\\",
			"|", "[", "{", "@", "`",
			"]", "}", ":", "*", ";",
			"+", "/", "?", ".", ">",
			",", "<"
		};

		public static string Create(IEnumerable<UnityEngine.U2D.SpriteAtlas> spriteAtlasList)
		{
			var builder = new StringBuilder();

			builder.AppendLine("using System;");
			builder.AppendLine("using UnityEngine;");
			builder.AppendLine("using UnityEngine.U2D;");
			builder.AppendLine();
			foreach (var spriteAtlas in spriteAtlasList)
			{
				var atlasName = spriteAtlas.name;
				var atlasNameSafe = RemoveInvalidChars(atlasName);

				builder.Append("\t").AppendLine("[Serializable]");
				builder.Append("\t").AppendFormat("public partial class {0} : AtlasBase", atlasNameSafe).AppendLine();
				builder.Append("\t").AppendLine("{");

				builder.Append("\t\t").AppendLine("[SerializeField] private SpriteAtlas _atlas = null;");
				builder.Append("\t\t").AppendLine();

				var sprites = new Sprite[spriteAtlas.spriteCount];
				spriteAtlas.GetSprites(sprites);

				foreach (var sprite in sprites)
				{
					var spriteName = sprite.name.Replace("(Clone)", string.Empty);
					var spriteNameSafe = RemoveInvalidChars(spriteName);

					builder.Append("\t\t")
						.AppendFormat(@"public const string _{0} = ""{1}"";", spriteNameSafe, spriteName).AppendLine();
				}

				builder.Append("\t\t").AppendLine();

				foreach (var sprite in sprites)
				{
					var spriteName = sprite.name.Replace("(Clone)", string.Empty);
					var spriteNameSafe = RemoveInvalidChars(spriteName);

					builder.Append("\t\t")
						.AppendFormat(@"public Sprite {0} {{ get {{ return _atlas.GetSprite( _{0} ); }} }}",
							spriteNameSafe).AppendLine();
				}

				builder.Append("\t\t").AppendLine();

				var names = sprites.Select(c => c.name).ToArray();

				foreach (var n in ToFormatDataSequence(names).Distinct(new FormatDataComparer()))
				{
					builder.Append("\t\t").AppendFormat(@"/// <summary>""{0}""</summary>", n.Format).AppendLine();
					builder.Append("\t\t").AppendFormat("public Sprite get_{0}( {1} ) {{ ", n.Name, n.Argument);
					builder.AppendFormat(@"return _atlas.GetSprite( string.Format( ""{0}"", {1} ) ); ", n.Format,
						n.Arg0);
					builder.AppendLine("}");
				}

				builder.Append("\t\t").AppendLine();
				builder.Append("\t\t").AppendFormat(@"public {0}() {{}}", atlasNameSafe).AppendLine();
				builder.Append("\t\t").AppendLine();
				builder.Append("\t\t")
					.AppendFormat(@"public {0}( SpriteAtlas atlas ) {{ _atlas = atlas; }}", atlasNameSafe)
					.AppendLine();
				builder.Append("\t\t").AppendLine();

				builder.Append("\t").AppendLine("}");
				builder.Append("\t").AppendLine();
			}

			return builder.ToString();
		}

		private static string RemoveInvalidChars(string str)
		{
			Array.ForEach(INVALID_CHARS, c => str = str.Replace(c, string.Empty));
			return str;
		}

		private static IEnumerable<FormatData> ToFormatDataSequence(IEnumerable<string> spriteNames)
		{
			foreach (var n in GetFormattableTextList("_", spriteNames.ToArray()))
			{
				var matches = Regex.Matches(n, @"{[0-9]}");

				if (matches.Count <= 0) continue;

				var safeLabel = Regex.Replace(n, @"{[0-9]}", string.Empty);

				if (string.IsNullOrEmpty(safeLabel)) continue;

				yield return new FormatData(matches.Count, n, safeLabel);
			}
		}

		private static string[] GetFormattableTextList(string separator, params string[] texts)
		{
			var list = new List<string>();

			for (int i = 0; i < texts.Length - 1; i++)
			{
				var separatedText1 = texts[i].Split(new[] { separator }, StringSplitOptions.None).ToArray();

				for (int j = i + 1; j < texts.Length; j++)
				{
					var separatedText2 = texts[j].Split(new[] { separator }, StringSplitOptions.None).ToArray();

					int argumentCount = 0;

					var builder = new StringBuilder();

					for (int k = 0; k < Math.Min(separatedText1.Length, separatedText2.Length); k++)
					{
						var text1 = separatedText1[k];
						var text2 = separatedText2[k];

						if (text1 == text2)
						{
							builder.Append(text1);
						}
						else
						{
							builder.AppendFormat("{{{0}}}", argumentCount++);
						}

						builder.Append("_");
					}

					builder = builder.Remove(builder.Length - 1, 1);

					list.Add(builder.ToString());
				}
			}

			return list.Distinct().ToArray();
		}
	}
}