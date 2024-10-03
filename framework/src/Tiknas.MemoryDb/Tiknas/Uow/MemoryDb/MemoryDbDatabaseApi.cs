using Tiknas.Domain.Repositories.MemoryDb;

namespace Tiknas.Uow.MemoryDb;

public class MemoryDbDatabaseApi : IDatabaseApi
{
    public IMemoryDatabase Database { get; }

    public MemoryDbDatabaseApi(IMemoryDatabase database)
    {
        Database = database;
    }
}
