using System;
using System.Threading.Tasks;
using MySqlConnector;
using Tiknas.Data;
using Tiknas.DependencyInjection;

namespace Tiknas.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class MySqlConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new TiknasConnectionStringCheckResult();
        var connString = new MySqlConnectionStringBuilder(connectionString)
        {
            ConnectionLifeTime = 1
        };

        var oldDatabaseName = connString.Database;
        connString.Database = "mysql";

        try
        {
            await using var conn = new MySqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
}
