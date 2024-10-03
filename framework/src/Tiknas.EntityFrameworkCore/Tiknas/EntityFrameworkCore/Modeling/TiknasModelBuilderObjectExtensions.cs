using Microsoft.EntityFrameworkCore;
using Tiknas.ObjectExtending;

namespace Tiknas.EntityFrameworkCore.Modeling;

public static class TiknasModelBuilderObjectExtensions
{
    public static void TryConfigureObjectExtensions<TDbContext>(this ModelBuilder modelBuilder)
        where TDbContext : DbContext
    {
        ObjectExtensionManager.Instance.ConfigureEfCoreDbContext<TDbContext>(modelBuilder);
    }
}
