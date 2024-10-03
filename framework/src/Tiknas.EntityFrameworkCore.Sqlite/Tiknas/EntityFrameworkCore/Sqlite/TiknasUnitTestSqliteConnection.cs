using System.Threading;
using Microsoft.Data.Sqlite;
using Tiknas.Threading;

namespace Tiknas.EntityFrameworkCore.Sqlite;

/// <summary>
/// This class is for unit testing purposes.
/// It prevents exceptions in concurrent testing because Sqlite is not thread-safe.
/// </summary>
public class TiknasUnitTestSqliteConnection : SqliteConnection
{
    public TiknasUnitTestSqliteConnection(string connectionString)
        : base(connectionString)
    {
    }

    public override SqliteCommand CreateCommand()
    {
        return new TiknasSqliteCommand
        {
            Connection = this,
            CommandTimeout = DefaultTimeout,
            Transaction = Transaction
        };
    }
}

internal class TiknasSqliteCommand : SqliteCommand
{
    private readonly static SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

    public override SqliteConnection? Connection
    {
        get => base.Connection;
        set
        {
            using (Semaphore.Lock())
            {
                base.Connection = value;
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        using (Semaphore.Lock())
        {
            base.Dispose(disposing);
        }
    }
}
