/*
 * Created by SharpDevelop.
 * User: User
 * Date: 13.04.2022
 * Time: 15:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KSyusha
{
	/// <summary>
	/// Description of Extension.
	/// </summary>
	public static class Extension
	{		
		// TODO: Python-specific hack!
		private static HashSet<string> reserved_identifiers = new HashSet<string>() {
			"from",
			"and",
			"or",
			"not",
			"import",
			"global",
			"local",
			"return",
			"break",
			"except",
			//"",
		};
		
		public static bool IsBeeObfuscated(this string name) {
			// TODO: very simple but should work
			return name.All(char.IsUpper) && (name.Length >= 10 && name.Length <= 15);
		}
		
		// Stolen from EF Core
		public static string ToSnakeCase(this string name)
		{
			if (string.IsNullOrEmpty(name))
				return name;

			var builder = new StringBuilder(name.Length + Math.Min(2, name.Length / 5));
			var previousCategory = default(UnicodeCategory?);

			for (var currentIndex = 0; currentIndex < name.Length; currentIndex++) {
				var currentChar = name[currentIndex];
				if (currentChar == '_') {
					builder.Append('_');
					previousCategory = null;
					continue;
				}

				var currentCategory = char.GetUnicodeCategory(currentChar);
				switch (currentCategory) {
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.TitlecaseLetter:
						if (previousCategory == UnicodeCategory.SpaceSeparator ||
						      previousCategory == UnicodeCategory.LowercaseLetter ||
						      previousCategory != UnicodeCategory.DecimalDigitNumber &&
						      previousCategory != null &&
						      currentIndex > 0 &&
						      currentIndex + 1 < name.Length &&
						      char.IsLower(name[currentIndex + 1])) {
							builder.Append('_');
						}

						currentChar = char.ToLower(currentChar);
						break;

					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.DecimalDigitNumber:
						if (previousCategory == UnicodeCategory.SpaceSeparator)
							builder.Append('_');
						break;

					default:
						if (previousCategory != null)
							previousCategory = UnicodeCategory.SpaceSeparator;
						continue;
				}

				builder.Append(currentChar);
				previousCategory = currentCategory;
			}

			return builder.ToString();
		}
		
		public static string __ToSnakeCase(this string text)
		{			
			if(text.Length < 2) {
				return text;
			}
			
			var sb = new StringBuilder();
			
			sb.Append(char.ToLowerInvariant(text[0]));
			
			for(int i = 1; i < text.Length; ++i) {
				char c = text[i];
				if(char.IsUpper(c)) {
					sb.Append('_');
					sb.Append(char.ToLowerInvariant(c));
				} else {
					sb.Append(c);
				}
			}
			
			return sb.ToString();
		}
		
		public static string Transform(this string text) {
			text = text.Split('.').Last();
			
			if (text.IsNormalSnakeCase()) {
				// Do nothing
			} else if (text.IsScreamingSnakeCase()) // SNAKE_CASE, just fix case
				text = text.ToLower();
			else
				text = text.ToSnakeCase().ToLower();
			
			text = text.TrimStart('_', ' ');
			
			if (reserved_identifiers.Contains(text)) {
				text = text + "_"; // TODO: Python hack!
			}
			
			return text;
		}
		
		public static bool IsScreamingSnakeCase(this string text) {
			return text.All(c => char.IsUpper(c) || char.IsDigit(c) || (c == '_'));
		}
		
		public static bool IsNormalSnakeCase(this string text) {
			return text.All(c => char.IsLower(c) || char.IsDigit(c) || (c == '_'));
		}
	}
}
