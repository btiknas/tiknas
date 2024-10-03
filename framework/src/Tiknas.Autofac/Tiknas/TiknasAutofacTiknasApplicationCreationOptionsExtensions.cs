using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Autofac;

namespace Tiknas;

public static class TiknasAutofacTiknasApplicationCreationOptionsExtensions
{
    public static void UseAutofac(this TiknasApplicationCreationOptions options)
    {
        options.Services.AddAutofacServiceProviderFactory();
    }

    public static TiknasAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services)
    {
        return services.AddAutofacServiceProviderFactory(new ContainerBuilder());
    }

    public static TiknasAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services, ContainerBuilder containerBuilder)
    {
        var factory = new TiknasAutofacServiceProviderFactory(containerBuilder);

        services.AddObjectAccessor(containerBuilder);
        services.AddSingleton((IServiceProviderFactory<ContainerBuilder>)factory);

        return factory;
    }
}
