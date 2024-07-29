FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Thunders.Tasks.WebApi/Thunders.Tasks.WebApi.csproj", "src/Thunders.Tasks.WebApi/"]
COPY ["src/Thunders.Tasks.Application/Thunders.Tasks.Application.csproj", "src/Thunders.Tasks.Application/"]
COPY ["src/Thunders.Tasks.Core/Thunders.Tasks.Core.csproj", "src/Thunders.Tasks.Core/"]
COPY ["src/Thunders.Tasks.Infrastructure/Thunders.Tasks.Infrastructure.csproj", "src/Thunders.Tasks.Infrastructure/"]
RUN dotnet restore "./src/Thunders.Tasks.WebApi/Thunders.Tasks.WebApi.csproj"
COPY . .
WORKDIR "/src/src//Thunders.Tasks.WebApi"
RUN dotnet build "./Thunders.Tasks.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Thunders.Tasks.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thunders.Tasks.WebApi.dll"]