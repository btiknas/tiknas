using System;
using Autofac;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Tiknas;
using Tiknas.AspNetCore.Components.WebAssembly;

namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class TiknasWebAssemblyApplicationCreationOptionsAutofacExtensions
{
    public static void UseAutofac(
        [NotNull] this TiknasWebAssemblyApplicationCreationOptions options,
        Action<ContainerBuilder>? configure = null)
    {
        options.HostBuilder.Services.AddAutofacServiceProviderFactory();
        options.HostBuilder.ConfigureContainer(
            options.HostBuilder.Services.GetSingletonInstance<IServiceProviderFactory<ContainerBuilder>>(),
            configure
        );
    }
}
