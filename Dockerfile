# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Define o ambiente padrão como Development (pode ser sobrescrito no docker run)
ENV ASPNETCORE_ENVIRONMENT=Development

# Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish AIChatApp.Api -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
# EXPOSE 443

ENTRYPOINT ["dotnet", "AIChatApp.Api.dll"]