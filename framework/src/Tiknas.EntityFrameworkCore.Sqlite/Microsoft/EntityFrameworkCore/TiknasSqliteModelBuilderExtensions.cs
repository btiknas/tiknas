using Tiknas.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class TiknasSqliteModelBuilderExtensions
{
    public static void UseSqlite(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.Sqlite);
    }
}
