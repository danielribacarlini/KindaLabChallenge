# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./FoodTruck.Api.csproj"
RUN dotnet build "./FoodTruck.Api.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "./FoodTruck.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodTruck.Api.dll"]