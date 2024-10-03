using Tiknas.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class TiknasSqlServerModelBuilderExtensions
{
    public static void UseSqlServer(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.SqlServer);
    }
}
