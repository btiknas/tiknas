using Tiknas.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class TiknasOracleModelBuilderExtensions
{
    public static void UseOracle(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.Oracle);
    }
}
