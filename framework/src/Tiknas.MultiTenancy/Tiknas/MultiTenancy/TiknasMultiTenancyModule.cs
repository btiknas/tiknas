using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Data;
using Tiknas.EventBus.Abstractions;
using Tiknas.Modularity;
using Tiknas.MultiTenancy.ConfigurationStore;
using Tiknas.Security;
using Tiknas.Settings;

namespace Tiknas.MultiTenancy;

[DependsOn(
    typeof(TiknasDataModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasSettingsModule),
    typeof(TiknasEventBusAbstractionsModule),
    typeof(TiknasMultiTenancyAbstractionsModule)
    )]
public class TiknasMultiTenancyModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentTenantAccessor>(AsyncLocalCurrentTenantAccessor.Instance);

        var configuration = context.Services.GetConfiguration();
        Configure<TiknasDefaultTenantStoreOptions>(configuration);

        Configure<TiknasSettingOptions>(options =>
        {
            options.ValueProviders.InsertAfter(t => t == typeof(GlobalSettingValueProvider), typeof(TenantSettingValueProvider));
        });

        Configure<TiknasTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Insert(0, new CurrentUserTenantResolveContributor());
        });
    }
}
