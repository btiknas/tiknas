using Tiknas.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class TiknasPostgreSqlModelBuilderExtensions
{
    public static void UsePostgreSql(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.PostgreSql);
    }
}
