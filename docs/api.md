# API Documentation

Base URL (local): `https://localhost:<port>`

## Reader Endpoints

- `POST /api/reader` - Create reader
- `GET /api/reader/{readerId}` - Get reader by id
- `GET /api/reader` - Get all readers
- `PUT /api/reader` - Update reader
- `DELETE /api/reader/{readerId}` - Delete reader

### Reader payload

```json
{
  "id": "00000000-0000-0000-0000-000000000001",
  "firstName": "Ali",
  "lastName": "Valiyev",
  "age": 21,
  "books": []
}
```

## Book Endpoints

- `POST /api/book` - Create book
- `GET /api/book/{bookId}` - Get book by id
- `GET /api/book` - Get all books
- `PUT /api/book` - Update book
- `DELETE /api/book/{bookId}` - Delete book

### Book payload

```json
{
  "id": "00000000-0000-0000-0000-000000000010",
  "name": "Clean Code",
  "author": "Robert C. Martin",
  "genre": "Programming",
  "readerId": "00000000-0000-0000-0000-000000000001"
}
```

## ReaderBook Endpoint

- `GET /api/readerbook/{readerId}`

Returns:

```json
{
  "reader": { "id": "...", "firstName": "Ali", "lastName": "Valiyev", "age": 21 },
  "books": [
    { "id": "...", "name": "Clean Code", "author": "Robert C. Martin", "genre": "Programming", "readerId": "..." }
  ]
}
```

## Swagger

When running in development environment:

- Swagger UI: `/swagger`
- OpenAPI JSON: `/swagger/v1/swagger.json`
