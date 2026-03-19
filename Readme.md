## JLOrdaz.DapperDataMSSQL

Lightweight helper to execute SQL Server stored procedures using Dapper and Microsoft.Data.SqlClient with a simple DI registration.

Supported target frameworks:

- .NET 8 (`net8.0`)
- .NET 10 (`net10.0`)

### Install

```bash
dotnet add package JLOrdaz.DapperDataMSSQL
```

### Configuration
Add your connection string in `appsettings.json` (or environment/config provider of your choice):

```json
{
  "ConnectionStrings": {
    "DB": "Server=.;Database=MyDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Usage with Dependency Injection (recommended)
Register the library in your DI container and inject `ISQLDataAccess` where needed.

`Program.cs` (ASP.NET Core):
```csharp
var builder = WebApplication.CreateBuilder(args);

// Option 1: Register using connection string name from configuration
builder.Services.AddDapperDataMSSQL(useConnectionStringName: true, connectionStringOrName: "DB");

// Option 2: Register using direct connection string
// builder.Services.AddDapperDataMSSQL(
//     useConnectionStringName: false, 
//     connectionStringOrName: "Server=.;Database=MyDb;Trusted_Connection=True;TrustServerCertificate=True;"
// );

var app = builder.Build();
app.Run();
```

**Parameters:**
- `useConnectionStringName`: If `true`, looks up the connection string in configuration. If `false`, uses the value directly as connection string.
- `connectionStringOrName`: The configuration key name (if `useConnectionStringName` is `true`) or the actual connection string (if `false`).

Example repository/service:
```csharp
using JLOrdaz.DapperDataMSSQL;

public sealed class UserRepository
{
    private readonly ISQLDataAccess _db;

    public UserRepository(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<User>> GetUsersAsync() =>
        _db.LoadData<User, object>("dbo.Users_GetAll", new { });

    public Task<User?> GetUserAsync(int id) =>
        _db.LoadFirst<User, object>("dbo.Users_GetById", new { Id = id });

    public Task SaveUserAsync(User user) =>
        _db.SaveData("dbo.Users_Upsert", new { user.Id, user.Name });
}

// Example: stored procedure that returns two result sets (e.g., Users and Roles)
public Task ExampleLoadMultipleAsync(ISQLDataAccess db) =>
    // Assumes stored procedure 'dbo.UsersAndRoles_Get' returns users in first result set and roles in second
    // The new method returns a tuple with both enumerables
    db.LoadMultiple<User, Role, object>("dbo.UsersAndRoles_Get", new { });

// Example: execute stored procedure that returns a scalar value (e.g., newly inserted identity)
public Task<int?> CreateUserAndGetIdAsync(ISQLDataAccess db, User user) =>
    // Assumes stored procedure 'dbo.Users_Insert' inserts and returns the new Id as scalar
    db.ExecuteScalarAsync<int, object>("dbo.Users_Insert", new { user.Name });
```

Notes:
- The connection string is configured once during service registration via `AddDapperDataMSSQL`.
- `storeProcedure` is the stored procedure name (schema-qualified recommended).

### Dependencies
- Microsoft.Data.SqlClient
- Dapper

### License
MIT

