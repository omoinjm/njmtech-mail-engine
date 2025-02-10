# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Expose the web and ssl port
EXPOSE 80 
EXPOSE 443
EXPOSE 8080
EXPOSE 5135

COPY ["./src/Mail.Engine.Service.Api/Mail.Engine.Service.Api.csproj", "./"]
COPY ["./src/Mail.Engine.Service.Application/Mail.Engine.Service.Application.csproj", "./"]
COPY ["./src/Mail.Engine.Service.Core/Mail.Engine.Service.Core.csproj", "./"]
COPY ["./src/Mail.Engine.Service.Infrastructure/Mail.Engine.Service.Infrastructure.csproj", "./"]

RUN dotnet restore "./Mail.Engine.Service.Api.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./src/Mail.Engine.Service.Api/Mail.Engine.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/Mail.Engine.Service.Api/Mail.Engine.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Mail.Engine.Service.Api.dll"]
