# Library API

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet) ![Build](https://img.shields.io/badge/build-local-lightgrey) ![Tests](https://img.shields.io/badge/tests-unit-lightgrey) ![License](https://img.shields.io/badge/license-MIT-green)

A clean, production-ready REST API for library workflows, built with .NET 10 and The-Standard style layering.

## Language

- English (current)
- Uzbek → [README.uz.md](README.uz.md)

## Table of Contents

- Overview
- Features
- Tech Stack
- Project Architecture
- Project Structure
- Getting Started
- Quick Start
- Documentation
- Contributing
- License

## Overview

Library API manages core library entities:

- Reader
- Book
- ReaderBook (reader + related books projection)

It provides RESTful CRUD operations for Reader and Book with service-layer validation and database persistence.

## Features

- CRUD endpoints for Reader and Book
- ReaderBook endpoint for combined reader/books view
- Service-level validation and exception handling
- Entity Framework Core persistence with SQL Server
- Swagger/OpenAPI support
- Unit testing support with mocks and fluent assertions

## Tech Stack

- .NET 10 (net10.0)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- RESTFulSense
- xUnit
- Moq
- FluentAssertions

### Runtime/Package Versions

- EF Core SQL Server: `10.0.2`
- Swashbuckle.AspNetCore: `10.1.0`
- RESTFulSense: `3.2.0`

## Project Architecture

The API follows a layered design:

- Controllers: HTTP boundary and status mapping
- Services: business logic and validation
- Brokers: storage and logging abstraction
- Database: SQL Server persistence

For detailed architecture and flow, see [docs/architecture.md](docs/architecture.md).

## Project Structure

```text
Library/
├── README.md
├── README.uz.md
├── LICENSE
├── docs/
│   ├── architecture.md
│   ├── api.md
│   ├── diagrams.md
│   ├── testing.md
│   ├── deployment.md
│   └── troubleshooting.md
└── Library.Api/
    ├── Library.Api/
    ├── Library.Api.Tests.Unit/
    └── Library.Api.Infrastructure.Build/
```

## Getting Started

1. Clone repository
2. Configure connection string
3. Apply EF migrations
4. Run API
5. Open Swagger

## Quick Start

```bash
git clone <REPO_URL>
cd Library/Library.Api
dotnet restore ../Library.sln
dotnet build ../Library.sln
dotnet ef database update --project Library.Api/Library.Api.csproj
cd Library.Api
dotnet run
```

Swagger: `https://localhost:<port>/swagger`

## Documentation

- Full architecture → [docs/architecture.md](docs/architecture.md)
- Full API documentation → [docs/api.md](docs/api.md)
- Diagrams → [docs/diagrams.md](docs/diagrams.md)
- Testing guide → [docs/testing.md](docs/testing.md)
- Deployment guide → [docs/deployment.md](docs/deployment.md)
- Troubleshooting → [docs/troubleshooting.md](docs/troubleshooting.md)

## Contributing

Please use feature branches and small, focused PRs.

1. Fork repository
2. Create branch (`feature/<name>`)
3. Add/adjust tests when needed
4. Update docs for behavior changes
5. Open a pull request

## License

Licensed under the [MIT License](LICENSE).
