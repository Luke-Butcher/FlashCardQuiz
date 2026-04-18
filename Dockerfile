# -- Stage 1: Build & Test --
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copy solution and projects
COPY *.sln ./
COPY FlashCardQuiz/*.csproj ./FlashCardQuiz/
COPY FlashCardQuiz.Tests/*.csproj ./FlashCardQuiz.Tests/
RUN dotnet restore

# Copy everything else
COPY . .

# Run Unit Tests
RUN dotnet test FlashCardQuiz.Tests/FlashCardQuiz.Tests.csproj -c Release

# Publish the app
RUN dotnet publish FlashCardQuiz/FlashCardQuiz.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# -- Stage 2: Runtime --
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app

# Install dependencies for healthchecks and globalization
RUN apk add --no-cache curl icu-libs icu-data-full

COPY --from=build /app/publish .

# Create data directory and set permissions
RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app/data

# Configuration
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

USER $APP_UID

ENTRYPOINT ["dotnet", "FlashCardQuiz.dll"]
