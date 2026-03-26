# Deployment Guide

## Prerequisites

- .NET 10 SDK
- SQL Server instance
- Valid connection string

## Configuration

Update `DefaultConnection` in `appsettings.json` or environment variables.

## Publish

```bash
dotnet restore Library.sln
dotnet build Library.sln -c Release
dotnet publish Library.Api/Library.Api/Library.Api.csproj -c Release -o ./publish
```

## Database Migration

```bash
dotnet ef database update --project Library.Api/Library.Api/Library.Api.csproj
```

## Run Published App

```bash
./publish/Library.Api
```
