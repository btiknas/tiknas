using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Tiknas;
using Tiknas.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static async Task<ITiknasApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        [NotNull] this WebApplicationBuilder builder,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        return await builder.Services.AddApplicationAsync<TStartupModule>(options =>
        {
            options.Services.ReplaceConfiguration(builder.Configuration);
            optionsAction?.Invoke(options);
            if (options.Environment.IsNullOrWhiteSpace())
            {
                options.Environment = builder.Environment.EnvironmentName;
            }
        });
    }

    public static async Task<ITiknasApplicationWithExternalServiceProvider> AddApplicationAsync(
        [NotNull] this WebApplicationBuilder builder,
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        return await builder.Services.AddApplicationAsync(startupModuleType, options =>
        {
            options.Services.ReplaceConfiguration(builder.Configuration);
            optionsAction?.Invoke(options);
            if (options.Environment.IsNullOrWhiteSpace())
            {
                options.Environment = builder.Environment.EnvironmentName;
            }
        });
    }
}
