namespace Tiknas.EntityFrameworkCore;

public interface ITiknasEfCoreDbContext : IEfCoreDbContext
{
    void Initialize(TiknasEfCoreDbContextInitializationContext initializationContext);
}
