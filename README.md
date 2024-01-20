# CleanArchitecture-Sample1
 Learn clean architecture by practicing


## Getting Started

Launch the app:
```bash
cd src/API
dotnet run
```


## Database

To add a new migration from the root folder:

```bash
dotnet ef migrations add MigrationName --project src\Infrastructure --startup-project src\API --output-dir Database\Migrations
```

To update database from the root folder:
```bash
dotnet ef database update --project src\Infrastructure --startup-project src\API
```


# Technical references
## 1. Clean architecture
## 2. CQRS pattern
## 3. Mediator pattern (MediatR)
## 4. Unit of work & generic repository


