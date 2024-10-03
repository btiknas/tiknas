using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.TextTemplating;

[DependsOn(
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasLocalizationAbstractionsModule)
    )]
public class TiknasTextTemplatingCoreModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddProvidersAndContributors(context.Services);
    }

    private static void AutoAddProvidersAndContributors(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();
        var contentContributors = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(ITemplateDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }

            if (typeof(ITemplateContentContributor).IsAssignableFrom(context.ImplementationType))
            {
                contentContributors.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasTextTemplatingOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
            options.ContentContributors.AddIfNotContains(contentContributors);
        });
    }
}
