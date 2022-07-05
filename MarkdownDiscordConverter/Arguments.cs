using CommandLine;

namespace MarkdownDiscordConverter;

public class Arguments
{
    [Option('f', "file",
		Required = true,
		HelpText = "The file to convert")]
    public string InputFile { get; set; } = null!;

    [Option('o', "output",
		Required = true,
		HelpText = "The file to output to")]
    public string OutputFile { get; set; } = null!;
}
