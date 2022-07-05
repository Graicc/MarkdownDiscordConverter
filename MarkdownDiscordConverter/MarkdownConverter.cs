using System.Text;
using System.Text.RegularExpressions;

namespace MarkdownDiscordConverter;

internal class MarkdownConverter
{
	public List<Func<string, string>> Converters { get; set; } = new()
	{
		// H1 -> Bold + Underline
		(s) =>
		{
			if (s.StartsWith("# "))
			{
				return $"__**{s.Substring(2)}**__";
			}
			return s;
		},
		// H2 -> Bold
		(s) =>
		{
			if (s.StartsWith("## "))
			{
				return $"**{s.Substring(3)}**";
			}
			return s;
		},
		// Remove images
		(s) =>
		{
			Regex regex = new Regex("<img.*?>");
			return regex.Replace(s, "");
		},
		// Format links
		(s) =>
		{
			Regex regex = new Regex(@"\[(.*?)\]\((.*?)\)");
			return regex.Replace(s, "$1 (<$2>)");
		},
		// Discord only blocks
		(s) =>
		{
			Regex regex = new Regex(@"<!--\s*DISC-ONLY\s*(.*?)\s*-->");
			return regex.Replace(s, "$1");
		},
	};

	public string Convert(string markdown)
	{
		StringBuilder sb = new();
		markdown.ReplaceLineEndings().Split(Environment.NewLine).ToList().ForEach(line =>
		{
			if (String.IsNullOrEmpty(line))
			{
				sb.AppendLine();
			}
			else
			{
				Converters.ForEach(converter =>
				{
					line = converter(line);
				});

				if (!String.IsNullOrEmpty(line))
				{
					sb.AppendLine(line);
				}
			}
		});

		return sb.ToString();
	}
}
