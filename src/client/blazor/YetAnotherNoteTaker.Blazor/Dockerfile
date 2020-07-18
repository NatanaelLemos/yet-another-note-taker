FROM nginx:latest AS base
WORKDIR /usr/share/nginx/html
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/client/blazor", "client/blazor"]
COPY ["src/client/YetAnotherNoteTaker.Client.Common", "client/YetAnotherNoteTaker.Client.Common"]
COPY ["src/client/System.Nxlx.Observer", "client/System.Nxlx.Observer"]
COPY ["src/YetAnotherNoteTaker.Common", "YetAnotherNoteTaker.Common"]
RUN dotnet restore "client/blazor/YetAnotherNoteTaker.Blazor/YetAnotherNoteTaker.Blazor.csproj"

WORKDIR "/src/client/blazor/YetAnotherNoteTaker.Blazor"
RUN dotnet build "YetAnotherNoteTaker.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YetAnotherNoteTaker.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html