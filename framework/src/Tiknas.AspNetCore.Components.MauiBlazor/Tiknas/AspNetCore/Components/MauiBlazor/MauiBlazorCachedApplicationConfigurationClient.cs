using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Tiknas.AspNetCore.Mvc.Client;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.Components.MauiBlazor
{
    public class MauiBlazorCachedApplicationConfigurationClient : ICachedApplicationConfigurationClient, ISingletonDependency
    {
        protected TiknasApplicationConfigurationClientProxy ApplicationConfigurationClientProxy { get; }

        protected TiknasApplicationLocalizationClientProxy ApplicationLocalizationClientProxy { get; }

        protected ApplicationConfigurationCache Cache { get; }

        protected ICurrentTenantAccessor CurrentTenantAccessor { get; }

        public MauiBlazorCachedApplicationConfigurationClient(
            TiknasApplicationConfigurationClientProxy applicationConfigurationClientProxy,
            ApplicationConfigurationCache cache,
            ICurrentTenantAccessor currentTenantAccessor,
            AuthenticationStateProvider authenticationStateProvider,
            TiknasApplicationLocalizationClientProxy applicationLocalizationClientProxy)
        {
            ApplicationConfigurationClientProxy = applicationConfigurationClientProxy;
            Cache = cache;
            CurrentTenantAccessor = currentTenantAccessor;
            ApplicationLocalizationClientProxy = applicationLocalizationClientProxy;

            authenticationStateProvider.AuthenticationStateChanged += async _ => { await InitializeAsync(); };
        }

        public virtual async Task InitializeAsync()
        {
            var configurationDto = await ApplicationConfigurationClientProxy.GetAsync(
                new ApplicationConfigurationRequestOptions
                {
                    IncludeLocalizationResources = false
                }
            );

            var localizationDto = await ApplicationLocalizationClientProxy.GetAsync(
                new ApplicationLocalizationRequestDto
                {
                    CultureName = configurationDto.Localization.CurrentCulture.Name,
                    OnlyDynamics = true
                }
            );

            configurationDto.Localization.Resources = localizationDto.Resources;

            Cache.Set(configurationDto);

            CurrentTenantAccessor.Current = new BasicTenantInfo(
                configurationDto.CurrentTenant.Id,
                configurationDto.CurrentTenant.Name);
        }

        public virtual Task<ApplicationConfigurationDto> GetAsync()
        {
            return Task.FromResult(GetConfigurationByChecking());
        }

        public virtual ApplicationConfigurationDto Get()
        {
            return GetConfigurationByChecking();
        }

        private ApplicationConfigurationDto GetConfigurationByChecking()
        {
            var configuration = Cache.Get();
            if (configuration == null)
            {
                throw new TiknasException(
                        $"{nameof(MauiBlazorCachedApplicationConfigurationClient)} should be initialized before using it.");
            }

            return configuration;
        }
    }
}