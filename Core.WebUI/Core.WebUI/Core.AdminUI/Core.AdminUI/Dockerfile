# Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Sadece WebAPI ve bağlı projeleri kopyala
COPY . .

# Restore ve publish
RUN dotnet restore "Core.AdminUI/Core.AdminUI/Core.AdminUI.csproj"
RUN dotnet publish "Core.AdminUI/Core.AdminUI/Core.AdminUI.csproj" -c Debug -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","Core.AdminUI.dll"]
