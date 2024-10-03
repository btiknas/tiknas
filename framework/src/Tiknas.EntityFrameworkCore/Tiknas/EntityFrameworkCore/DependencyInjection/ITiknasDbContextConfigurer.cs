namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public interface ITiknasDbContextConfigurer
{
    void Configure(TiknasDbContextConfigurationContext context);
}

public interface ITiknasDbContextConfigurer<TDbContext>
    where TDbContext : TiknasDbContext<TDbContext>
{
    void Configure(TiknasDbContextConfigurationContext<TDbContext> context);
}
