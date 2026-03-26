# Troubleshooting

## `dotnet: command not found`

Install .NET 10 SDK and verify:

```bash
dotnet --info
```

## Database connection errors

- Check SQL Server availability
- Verify `DefaultConnection`
- Ensure user credentials and DB permissions are correct

## Migration issues

- Rebuild solution first
- Verify startup project and migration assembly
- Re-run `dotnet ef database update`

## Swagger not opening

- Ensure app runs in Development environment
- Verify middleware setup in `Program.cs`
- Open exact URL shown in terminal output
