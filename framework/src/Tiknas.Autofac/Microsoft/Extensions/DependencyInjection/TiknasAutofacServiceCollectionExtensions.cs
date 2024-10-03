using System;
using Autofac;
using JetBrains.Annotations;
using Tiknas;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasAutofacServiceCollectionExtensions
{
    [NotNull]
    public static ContainerBuilder GetContainerBuilder([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        var builder = services.GetObjectOrNull<ContainerBuilder>();
        if (builder == null)
        {
            throw new TiknasException($"Could not find ContainerBuilder. Be sure that you have called {nameof(TiknasAutofacTiknasApplicationCreationOptionsExtensions.UseAutofac)} method before!");
        }

        return builder;
    }

    public static IServiceProvider BuildAutofacServiceProvider([NotNull] this IServiceCollection services, Action<ContainerBuilder>? builderAction = null)
    {
        return services.BuildServiceProviderFromFactory(builderAction);
    }
}
