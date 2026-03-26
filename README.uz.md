# Library API

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet) ![Build](https://img.shields.io/badge/build-local-lightgrey) ![Tests](https://img.shields.io/badge/tests-unit-lightgrey) ![License](https://img.shields.io/badge/license-MIT-green)

Kutubxona jarayonlari uchun .NET 10 asosida yaratilgan, The-Standard uslubidagi qatlamli REST API.

## Mundarija

- Umumiy ma'lumot
- Imkoniyatlar
- Texnologiyalar
- Arxitektura
- Loyiha tuzilmasi
- Ishga tushirish
- Tezkor start
- Hujjatlar
- Hissa qo‘shish
- Litsenziya

## Umumiy ma'lumot

Library API quyidagi asosiy entitilar bilan ishlaydi:

- Reader
- Book
- ReaderBook (kitobxon va unga tegishli kitoblar ko‘rinishi)

Reader va Book uchun to‘liq CRUD, ReaderBook uchun birlashtirilgan ko‘rinish endpointi mavjud.

## Imkoniyatlar

- Reader va Book uchun CRUD endpointlar
- ReaderBook orqali kitobxon + kitoblar birga qaytarish
- Servis qatlamida validatsiya va exception handling
- SQL Server bilan EF Core persistence
- Swagger/OpenAPI
- Unit testlar (xUnit, Moq, FluentAssertions)

## Texnologiyalar

- .NET 10 (net10.0)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- RESTFulSense
- xUnit
- Moq
- FluentAssertions

### Asosiy paket versiyalari

- EF Core SQL Server: `10.0.2`
- Swashbuckle.AspNetCore: `10.1.0`
- RESTFulSense: `3.2.0`

## Arxitektura

Qatlamlar:

- Controller: HTTP endpointlar va status code mapping
- Service: biznes mantiq va validatsiya
- Broker: storage/logging abstraksiyasi
- Database: SQL Server

Batafsil: [docs/architecture.md](docs/architecture.md)

## Tezkor start

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

## Hujjatlar

- Arxitektura → [docs/architecture.md](docs/architecture.md)
- API hujjatlar → [docs/api.md](docs/api.md)
- Diagrammalar → [docs/diagrams.md](docs/diagrams.md)
- Test yo‘riqnomasi → [docs/testing.md](docs/testing.md)
- Deploy yo‘riqnomasi → [docs/deployment.md](docs/deployment.md)
- Muammolarni hal qilish → [docs/troubleshooting.md](docs/troubleshooting.md)

## Litsenziya

[MIT License](LICENSE)
