## JLOrdaz.DapperDataMSSQL

Lightweight helper to execute SQL Server stored procedures using Dapper and Microsoft.Data.SqlClient with a simple DI registration.

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

// Register JLOrdaz.DapperDataMSSQL services
builder.Services.AddDapperDataMSSQL();

var app = builder.Build();
app.Run();
```

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
        _db.LoadData<User, object>("dbo.Users_GetAll", new { }, "DB");

    public Task<User?> GetUserAsync(int id) =>
        _db.LoadFirst<User, object>("dbo.Users_GetById", new { Id = id }, "DB");

    public Task SaveUserAsync(User user) =>
        _db.SaveData("dbo.Users_Upsert", new { user.Id, user.Name }, "DB");
}
```

Notes:
- `storeProcedure` is the stored procedure name (schema-qualified recommended).
- `connectionString` is the name/key of the connection string in your configuration (e.g., `"DB"`).

### Dependencies
- Microsoft.Data.SqlClient
- Dapper

### License
MIT

