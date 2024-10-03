using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Tiknas.Authorization;
using Tiknas.Features.Localization;
using Tiknas.Localization;
using Tiknas.Localization.ExceptionHandling;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Validation;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Features;

[DependsOn(
    typeof(TiknasLocalizationModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasValidationModule),
    typeof(TiknasAuthorizationAbstractionsModule)
    )]
public class TiknasFeaturesModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(FeatureInterceptorRegistrar.RegisterIfNeeded);
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<TiknasFeatureOptions>(options =>
        {
            options.ValueProviders.Add<DefaultValueFeatureValueProvider>();
            options.ValueProviders.Add<EditionFeatureValueProvider>();
            options.ValueProviders.Add<TenantFeatureValueProvider>();
        });

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasFeatureResource>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasFeatureResource>("en")
                .AddVirtualJson("/Tiknas/Features/Localization");
        });

        Configure<TiknasExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Tiknas.Feature", typeof(TiknasFeatureResource));
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IFeatureDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasFeatureOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
