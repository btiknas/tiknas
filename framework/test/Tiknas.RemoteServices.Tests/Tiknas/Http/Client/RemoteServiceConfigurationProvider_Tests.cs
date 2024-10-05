using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.MultiTenancy;
using Tiknas.MultiTenancy.ConfigurationStore;
using Xunit;

namespace Tiknas.Http.Client;

public class RemoteServiceConfigurationProvider_Tests : TiknasRemoteServicesTestBase
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IRemoteServiceConfigurationProvider _remoteServiceConfigurationProvider;
    private readonly Guid _tenantAId = Guid.NewGuid();
    
    public RemoteServiceConfigurationProvider_Tests()
    {
        _currentTenant =  GetRequiredService<ICurrentTenant>();
        _remoteServiceConfigurationProvider = GetRequiredService<IRemoteServiceConfigurationProvider>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasRemoteServiceOptions>(options =>
        {
            options.RemoteServices[RemoteServiceConfigurationDictionary.DefaultName] = new RemoteServiceConfiguration("https://tiknas.de");
            options.RemoteServices["Identity"] = new RemoteServiceConfiguration("https://{{tenantName}}.tiknas.de");
            options.RemoteServices["Permission"] = new RemoteServiceConfiguration("https://{{tenantId}}.tiknas.de");
            options.RemoteServices["Setting"] = new RemoteServiceConfiguration("https://{0}.tiknas.de");
        });
        
        services.Configure<TiknasDefaultTenantStoreOptions>(options =>
        {
            options.Tenants =
            [
                new TenantConfiguration(_tenantAId, "TenantA")
            ];
        });
    }

    [Fact]
    public async Task GetConfigurationOrDefaultAsync()
    {
        _currentTenant.Id.ShouldBeNull();
        
        var defaultConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(RemoteServiceConfigurationDictionary.DefaultName);
        defaultConfiguration.BaseUrl.ShouldBe("https://tiknas.de");

        var identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
        identityConfiguration.BaseUrl.ShouldBe("https://tiknas.de");

        var permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
        permissionConfiguration.BaseUrl.ShouldBe("https://tiknas.de");
        
        var settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
        settingConfiguration.BaseUrl.ShouldBe("https://tiknas.de");

        using (_currentTenant.Change(_tenantAId, "TenantA"))
        { 
            defaultConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(RemoteServiceConfigurationDictionary.DefaultName);
            defaultConfiguration.BaseUrl.ShouldBe("https://tiknas.de");

            identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
            identityConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.tiknas.de");

            permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
            permissionConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Id}.tiknas.de");
            
            settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
            settingConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.tiknas.de");
        }
    }
}