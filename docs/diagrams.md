# Diagrams

## High-level Components

```text
Client -> Controllers -> Services -> Brokers -> SQL Server
                    \-> Exception Mapping -> HTTP Response
```

## Domain Model

```text
Reader (Id, FirstName, LastName, Age, Books)
   1  ----  *
Book (Id, Name, Author, Genre, ReaderId)

ReaderBook
- Reader Reader
- List<Book> Books
```

## Request Flow (Retrieve ReaderBook)

```text
GET /api/readerbook/{readerId}
  -> ReaderBookController
  -> ReaderService.RetrieveReaderByIdAsync(readerId)
  -> BookService.RetrieveAllBooks().Where(b => b.ReaderId == readerId)
  -> return { reader, books }
```
