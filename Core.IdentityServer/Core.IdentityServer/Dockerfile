FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./Core.IdentityServer/Core.IdentityServer.csproj"
RUN dotnet publish "./Core.IdentityServer/Core.IdentityServer.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","Core.IdentityServer.dll"]