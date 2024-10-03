using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.Security;
using Tiknas.Data;

namespace Tiknas.Settings;

[DependsOn(
    typeof(TiknasLocalizationAbstractionsModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasDataModule)
)]
public class TiknasSettingsModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSettingOptions>(options =>
        {
            options.ValueProviders.Add<DefaultValueSettingValueProvider>();
            options.ValueProviders.Add<ConfigurationSettingValueProvider>();
            options.ValueProviders.Add<GlobalSettingValueProvider>();
            options.ValueProviders.Add<UserSettingValueProvider>();
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(ISettingDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasSettingOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
