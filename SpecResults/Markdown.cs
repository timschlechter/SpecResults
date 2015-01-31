using System.Text.RegularExpressions;

namespace SpecResults
{
	public static class Markdown
	{
		private static readonly string[] BlockLevelTags =
		{
			"address", "article", "aside", "audio", "blockquote", "canvas",
			"dd", "div", "dl", "fieldset", "figcaption", "figure", "footer",
			"form", "h1", "h2", "h3", "h4", "h5", "h6", "header", "hgroup",
			"hr", "noscript", "ol", "output", "p", "pre", "section", "table",
			"tfoot", "ul", "video", "li"
		};

		public static string ToHtml(string markdown)
		{
			var md = new MarkdownSharp.Markdown();

			var result = md.Transform(markdown);

			// HACK: postprocessing to cleanup stuff
			result = Regex.Replace(result, "(\n)", "<br />");
			if (result.EndsWith("<br />"))
			{
				result = result.Remove(result.Length - 6);
			}

			foreach (var tag in BlockLevelTags)
			{
				result = result.Replace("<" + tag + "><br />", "<" + tag + ">");
				result = result.Replace("</" + tag + "><br />", "</" + tag + ">");
			}

			return result;
		}
	}
}