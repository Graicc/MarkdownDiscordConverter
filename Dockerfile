# Set the base image as the .NET 6.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/runtime:6.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Graic <https://github.com/graicc>"
LABEL repository="https://github.com/Graicc/MarkdownDiscordConverter"
LABEL homepage="https://github.com/Graicc/MarkdownDiscordConverter"

# Label as GitHub action
LABEL com.github.actions.name="Markdown Discord Converter"
# Limit to 160 characters
LABEL com.github.actions.description="Converts markdown files to a discord compatable format."
# See branding:
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="file-plus"
LABEL com.github.actions.color="green"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/MarkdownDiscordConverter.dll" ]
