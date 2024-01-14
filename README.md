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


