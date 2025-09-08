# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV ASPNETCORE_URLS=http://+:8090
WORKDIR /app
COPY --from=build /app .
EXPOSE 8090
ENTRYPOINT ["dotnet","CalculationsService.dll"]