# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Собираем базовый образ на основе аспнет-образа от MS
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Выставляем наружу только два порта (остальные будут недоступны)
EXPOSE 8080
EXPOSE 8081


# На этом слое мы будем собирать контейнер в релизной конфигурации, поэтому собираем на основе sdk
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/TUP.WebApi/
COPY ["TUP.WebApi.csproj", "TUP.WebApi/"]
# Восстанавливаем нугеты
RUN dotnet restore "./TUP.WebApi/TUP.WebApi.csproj"
COPY .  "TUP.WebApi/"
# Собственно, сборка в конфигурации Release
RUN dotnet build "./TUP.WebApi/TUP.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build 

# Публикуем сервис (т.е. выделяем из него только то, что нужно на продакшене)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TUP.WebApi/TUP.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с инструкцией по запуску приложения
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "./TUP.WebApi/TUP.WebApi.csproj"]