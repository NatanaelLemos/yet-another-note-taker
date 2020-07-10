FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/server", "server"]
COPY ["src/YetAnotherNoteTaker.Common", "YetAnotherNoteTaker.Common"]
RUN dotnet restore "server/YetAnotherNoteTaker.Server/YetAnotherNoteTaker.Server.csproj"

WORKDIR "/src/server/YetAnotherNoteTaker.Server"
RUN dotnet build "YetAnotherNoteTaker.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YetAnotherNoteTaker.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YetAnotherNoteTaker.Server.dll"]