#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SlackBot.WebHooks/SlackBot.WebHooks.csproj", "SlackBot.WebHooks/"]
COPY ["SlackBot.WebHooks.Receivers.Slack/SlackBot.WebHooks.Receivers.Slack.csproj", "SlackBot.WebHooks.Receivers.Slack/"]
RUN dotnet restore "SlackBot.WebHooks/SlackBot.WebHooks.csproj"
COPY . .
WORKDIR "/src/SlackBot.WebHooks"
RUN dotnet build "SlackBot.WebHooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SlackBot.WebHooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SlackBot.WebHooks.dll"]
