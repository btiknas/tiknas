using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Tiknas.Data;
using Tiknas.DependencyInjection;

namespace Tiknas.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class SqlServerConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new TiknasConnectionStringCheckResult();
        var connString = new SqlConnectionStringBuilder(connectionString)
        {
            ConnectTimeout = 1
        };

        var oldDatabaseName = connString.InitialCatalog;
        connString.InitialCatalog = "master";

        try
        {
            await using var conn = new SqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception e)
        {
            return result;
        }
    }
}
