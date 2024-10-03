using System;
using System.Threading.Tasks;
using Devart.Data.Oracle;
using Tiknas.Data;
using Tiknas.DependencyInjection;

namespace Tiknas.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class OracleDevartConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new TiknasConnectionStringCheckResult();
        var connString = new OracleConnectionStringBuilder(connectionString)
        {
            ConnectionTimeout = 1
        };

        try
        {
            await using var conn = new OracleConnection(connString.ConnectionString);
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
