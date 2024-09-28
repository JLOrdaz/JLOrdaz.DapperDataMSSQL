## JLOrdaz.DapperDataMSSL

The Class `SQLDataAccess` handling database operations within the application. This includes connecting to the SQL database, executing queries, and managing data retrieval and updates. Below is a brief overview of the key functionalities provided by this file:

### Key Functionalities

1. **Database Connection**:
   - Establishes a connection to the SQL database using connection strings.
   - Ensures secure and efficient database connectivity.

2. **Data Retrieval**:
   - Executes SQL queries to fetch data from the database.
   - Maps the retrieved data to appropriate data models.

3. **Data Insertion and Updates**:
   - Handles the insertion of new records into the database.
   - Manages updates to existing records ensuring data integrity.

4. **Error Handling**:
   - Implements robust error handling to manage database-related exceptions.
   - Logs errors for debugging and maintenance purposes.

### Example Usage

Here is a basic example of how to use the `SQLDataAccess` class to retrieve data:

```csharp
// Create an instance of SQLDataAccess
SQLDataAccess db = new SQLDataAccess();

// Define a query to fetch data
string query = "SELECT * FROM Users";

// Execute the query and retrieve the results
var users = db.LoadData<UserModel>(query);

// Process the retrieved data
foreach (var user in users)
{
    Console.WriteLine($"User ID: {user.Id}, User Name: {user.Name}");
}
```

### Dependencies
* **System.Data.SqlClient**: Used for SQL database connectivity.
* **Dapper**: (If applicable) Used for object mapping and query execution.

Configuration
Ensure that the connection string is correctly configured in the application's configuration file (e.g., appsettings.json or web.config).

```csharp
{
  "ConnectionStrings": {
    "DB": "YourConnectionStringHere"
  }
}
```

