# MarkdownDiscordConvereter

Markdown to Discord message format converter

## Purpose

This program converts markdown files into files compatible with Discord's subset of markdown for messages. This can be used send webhook messages based of markdown files, such as [automatically posting project releases in a channel](https://github.com/Gorilla-Tag-Modding-Group/gorilla-tag-build-tools/blob/main/actions/publish/action.yml).

## Usage

This program can be used in three ways:

- As a command line application, with `-f` as the input file and `-o` as the output file.
- As a docker container `ghcr.io/graicc/markdowndiscordconverter:latest` with the same arguments
- As a Github Action:

```yml
  - uses: Graicc/MarkdownDiscordConverter@master
    with:
      file: input.md
      output: output.txt
```

## Transformations

The program applies the following transformations:

- `# Heading` to `__**Heading**__`
- `## Heading` to `**Heading**`
- `[Example](example.com)` to `Example (<example.com>)`
- `<!-- DISC-ONLY Some text -->` to `Some text`
- Removes `<img>` tags
