# Testing Guide

## Test Project

- `Library.Api/Library.Api.Tests.Unit`

## Covered Areas

- Reader service logic and validations
- Book service logic and validations
- Exception paths and dependency/service wrapping

## Run Tests

```bash
dotnet test Library.sln
```

## Notes

- Tests rely on mocked brokers (Moq)
- Assertions use FluentAssertions
- Test style follows Arrange/Act/Assert with clear Given/When/Then comments
