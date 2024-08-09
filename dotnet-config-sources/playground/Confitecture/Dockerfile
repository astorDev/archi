FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
EXPOSE 8080
WORKDIR /app

COPY . .
RUN dotnet publish "Confitecture.csproj" -c Release -o /app/publish

WORKDIR /app/publish
ENTRYPOINT dotnet Confitecture.dll