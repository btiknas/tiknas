using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.Validation.Localization;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Validation;

[DependsOn(
    typeof(TiknasValidationAbstractionsModule),
    typeof(TiknasLocalizationModule)
    )]
public class TiknasValidationModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(ValidationInterceptorRegistrar.RegisterIfNeeded);
        AutoAddObjectValidationContributors(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasValidationResource>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasValidationResource>("en")
                .AddVirtualJson("/Tiknas/Validation/Localization");
        });
    }

    private static void AutoAddObjectValidationContributors(IServiceCollection services)
    {
        var contributorTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IObjectValidationContributor).IsAssignableFrom(context.ImplementationType))
            {
                contributorTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasValidationOptions>(options =>
        {
            options.ObjectValidationContributors.AddIfNotContains(contributorTypes);
        });
    }
}
