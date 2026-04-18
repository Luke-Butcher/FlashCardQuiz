# -- Stage 1: Build & Test --
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and projects
COPY *.sln ./
COPY FlashCardQuiz/*.csproj ./FlashCardQuiz/
COPY FlashCardQuiz.Tests/*.csproj ./FlashCardQuiz.Tests/
RUN dotnet restore

# Copy everything else
COPY . .

# Run Unit Tests - Build will fail if tests fail
RUN dotnet test FlashCardQuiz.Tests/FlashCardQuiz.Tests.csproj -c Release

# Publish the app
RUN dotnet publish FlashCardQuiz/FlashCardQuiz.csproj \
    -c Release \
    --self-contained false \
    -o /app/publish

# -- Stage 2: Runtime --
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Create data directory for SQLite persistence
RUN mkdir -p /data && chown -R app:app /data
USER app

COPY --from=build /app/publish .

# Configuration
ENV ASPNETCORE_URLS=http://+:8080
ENV DB_PATH=/data/flashcards.db

EXPOSE 8080

ENTRYPOINT ["dotnet", "FlashCardQuiz.dll"]
