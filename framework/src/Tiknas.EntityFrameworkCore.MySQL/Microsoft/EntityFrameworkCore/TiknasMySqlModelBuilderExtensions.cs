using Tiknas.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class TiknasMySqlModelBuilderExtensions
{
    public static void UseMySQL(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.MySql);
    }
}
