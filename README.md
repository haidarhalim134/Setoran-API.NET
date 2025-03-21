## Adding a New Migration
Run the following command to create a new migration:
```sh
 dotnet ef migrations add <MigrationName>
```
Replace `<MigrationName>` with a descriptive name (e.g., `AddNewTable`).

## Applying Migrations to the Database
To apply the latest migrations and update the database, run:
```sh
 dotnet ef database update
```
