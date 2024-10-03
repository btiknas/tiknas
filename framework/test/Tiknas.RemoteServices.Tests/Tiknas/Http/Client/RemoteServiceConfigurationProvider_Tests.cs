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
            options.RemoteServices[RemoteServiceConfigurationDictionary.DefaultName] = new RemoteServiceConfiguration("https://tiknas.io");
            options.RemoteServices["Identity"] = new RemoteServiceConfiguration("https://{{tenantName}}.tiknas.io");
            options.RemoteServices["Permission"] = new RemoteServiceConfiguration("https://{{tenantId}}.tiknas.io");
            options.RemoteServices["Setting"] = new RemoteServiceConfiguration("https://{0}.tiknas.io");
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
        defaultConfiguration.BaseUrl.ShouldBe("https://tiknas.io");

        var identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
        identityConfiguration.BaseUrl.ShouldBe("https://tiknas.io");

        var permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
        permissionConfiguration.BaseUrl.ShouldBe("https://tiknas.io");
        
        var settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
        settingConfiguration.BaseUrl.ShouldBe("https://tiknas.io");

        using (_currentTenant.Change(_tenantAId, "TenantA"))
        { 
            defaultConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(RemoteServiceConfigurationDictionary.DefaultName);
            defaultConfiguration.BaseUrl.ShouldBe("https://tiknas.io");

            identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
            identityConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.tiknas.io");

            permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
            permissionConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Id}.tiknas.io");
            
            settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
            settingConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.tiknas.io");
        }
    }
}