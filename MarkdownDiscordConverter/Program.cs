using CommandLine;
using MarkdownDiscordConverter;

var parser = Parser.Default.ParseArguments<Arguments>(args);
parser.WithParsed(options =>
{
	File.WriteAllText(options.OutputFile, new MarkdownConverter().Convert(File.ReadAllText(options.InputFile)));
});
