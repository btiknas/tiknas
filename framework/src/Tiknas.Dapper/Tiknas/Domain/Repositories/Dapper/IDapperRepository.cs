using System;
using System.Data;
using System.Threading.Tasks;

namespace Tiknas.Domain.Repositories.Dapper;

public interface IDapperRepository
{
    [Obsolete("Use GetDbConnectionAsync method.")]
    IDbConnection DbConnection { get; }

    Task<IDbConnection> GetDbConnectionAsync();

    [Obsolete("Use GetDbTransactionAsync method.")]
    IDbTransaction? DbTransaction { get; }

    Task<IDbTransaction?> GetDbTransactionAsync();
}
