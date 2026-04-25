FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MiniEditore.sln", "./"]
COPY ["src/App.Application/App.Application.csproj", "src/App.Application/"]
COPY ["src/App.Domain/App.Domain.csproj", "src/App.Domain/"]
COPY ["src/App.Web/App.Web.csproj", "src/App.Web/"]

RUN dotnet restore "src/App.Web/App.Web.csproj"

COPY . .

RUN dotnet publish "src/App.Web/App.Web.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ApiBaseAddress=http://api:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "App.Web.dll"]