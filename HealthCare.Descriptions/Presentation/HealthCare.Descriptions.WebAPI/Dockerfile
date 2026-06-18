# Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Sadece WebAPI ve bağlı projeleri kopyala
COPY . .

# Restore ve publish
RUN dotnet restore "Presentation/HealthCare.Descriptions.WebAPI/HealthCare.Descriptions.WebAPI.csproj"
RUN dotnet publish "Presentation/HealthCare.Descriptions.WebAPI/HealthCare.Descriptions.WebAPI.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","HealthCare.Descriptions.WebAPI.dll"]
