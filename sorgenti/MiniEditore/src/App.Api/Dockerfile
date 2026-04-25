FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MiniEditore.sln", "./"]
COPY ["src/App.Domain/App.Domain.csproj", "src/App.Domain/"]
COPY ["src/App.Application/App.Application.csproj", "src/App.Application/"]
COPY ["src/App.Infrastructure/App.Infrastructure.csproj", "src/App.Infrastructure/"]
COPY ["src/App.Api/App.Api.csproj", "src/App.Api/"]

RUN dotnet restore "src/App.Api/App.Api.csproj"

COPY . .

RUN dotnet publish "src/App.Api/App.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "App.Api.dll"]