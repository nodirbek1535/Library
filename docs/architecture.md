# Architecture

## Layered Design (The-Standard style)

This project uses a clear separation of concerns:

1. **Controllers**
   - Define HTTP routes
   - Call service interfaces
   - Map domain exceptions to HTTP responses

2. **Services (Foundations)**
   - Implement business rules and validations
   - Coordinate storage operations
   - Wrap and standardize exceptions

3. **Brokers**
   - **StorageBroker**: EF Core `DbContext` + CRUD contracts
   - **LoggingBroker**: centralized logging abstraction

4. **Database**
   - SQL Server + EF Core migrations

## Entity Relationships

- `Reader` (1) -> (many) `Book`
- `ReaderBook` is a projection model used to return a reader together with books.

## Runtime Flow Example (POST /api/book)

- Controller receives request
- BookService validates input
- StorageBroker writes to DB
- Controller returns result or mapped error
