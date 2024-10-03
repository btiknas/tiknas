using System.Threading.Tasks;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Tiknas.AspNetCore.Mvc.Client;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Threading;

namespace Tiknas.Maui.Client;

public class MauiCachedApplicationConfigurationClient :
    ICachedApplicationConfigurationClient,
    ISingletonDependency
{
    protected TiknasApplicationConfigurationClientProxy ApplicationConfigurationClientProxy { get; }
    protected TiknasApplicationLocalizationClientProxy ApplicationLocalizationClientProxy { get; }
    protected ApplicationConfigurationCache Cache { get; }
    protected ICurrentTenantAccessor CurrentTenantAccessor { get; }

    public MauiCachedApplicationConfigurationClient(
        TiknasApplicationConfigurationClientProxy applicationConfigurationClientProxy,
        TiknasApplicationLocalizationClientProxy applicationLocalizationClientProxy,
        ApplicationConfigurationCache cache,
        ICurrentTenantAccessor currentTenantAccessor)
    {
        ApplicationConfigurationClientProxy = applicationConfigurationClientProxy;
        ApplicationLocalizationClientProxy = applicationLocalizationClientProxy;
        CurrentTenantAccessor = currentTenantAccessor;
        Cache = cache;
    }

    public virtual async Task<ApplicationConfigurationDto> InitializeAsync()
    {
        var configurationDto = await ApplicationConfigurationClientProxy.GetAsync(
            new ApplicationConfigurationRequestOptions
            {
                IncludeLocalizationResources = false,
            });

        var localizationDto = await ApplicationLocalizationClientProxy.GetAsync(
            new ApplicationLocalizationRequestDto
            {
                CultureName = configurationDto.Localization.CurrentCulture.Name,
                OnlyDynamics = true
            }
        );

        configurationDto.Localization.Resources = localizationDto.Resources;

        CurrentTenantAccessor.Current = new BasicTenantInfo(
            configurationDto.CurrentTenant.Id,
            configurationDto.CurrentTenant.Name);

        Cache.Set(configurationDto);

        return configurationDto;
    }

    public virtual ApplicationConfigurationDto Get()
    {
        return AsyncHelper.RunSync(GetAsync);
    }

    public virtual async Task<ApplicationConfigurationDto> GetAsync()
    {
        var configurationDto = Cache.Get();
        if (configurationDto is null)
        {
            return await InitializeAsync();
        }

        return configurationDto;
    }
}
