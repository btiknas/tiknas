using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Tiknas.Data;
using Tiknas.DependencyInjection;

namespace Tiknas.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class SqliteConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new TiknasConnectionStringCheckResult();

        try
        {
            await using var conn = new SqliteConnection(connectionString);
            await conn.OpenAsync();
            result.Connected = true;
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
