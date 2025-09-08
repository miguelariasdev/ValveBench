# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
# URL del gRPC por DNS del servicio en compose
ENV CALC_URL=http://calculationsservice:8090
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet","OperationsApi.dll"]