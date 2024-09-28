namespace JLOrdaz.DapperDataMSSQL;

public interface ISQLDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionId = "DB");
    Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionId = "DB");
    Task SaveData<T>(string storeProcedure, T parameters, string connectionId = "DB");
}
