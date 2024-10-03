using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.MultiTenancy.ConfigurationStore;
using Xunit;

namespace Tiknas.MultiTenancy;

public class MultiTenantUrlProivder_Tests : MultiTenancyTestBase
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IMultiTenantUrlProvider _multiTenantUrlProvider;

    private readonly Guid _tenantAId = Guid.NewGuid();

    public MultiTenantUrlProivder_Tests()
    {
        _currentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
        _multiTenantUrlProvider = ServiceProvider.GetRequiredService<IMultiTenantUrlProvider>();
    }
    protected override void BeforeAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasDefaultTenantStoreOptions>(options =>
        {
            options.Tenants =
            [
                new TenantConfiguration(_tenantAId, "TenantA")
            ];
        });
    }

    [Fact]
    public async Task GetUrlAsync()
    {
        var tenantNameHolderUrl = "https://{{tenantName}}.tiknas.io";
        var tenantIdHolderUrl = "https://{{tenantId}}.tiknas.io";
        var tenantHolderUrl = "https://{0}.tiknas.io";
        var hostUrl = "https://tiknas.io";
        
        _currentTenant.Id.ShouldBeNull();
        (await _multiTenantUrlProvider.GetUrlAsync(tenantNameHolderUrl)).ShouldBe(hostUrl);
        (await _multiTenantUrlProvider.GetUrlAsync(tenantIdHolderUrl)).ShouldBe(hostUrl);
        (await _multiTenantUrlProvider.GetUrlAsync(tenantHolderUrl)).ShouldBe(hostUrl);
        (await _multiTenantUrlProvider.GetUrlAsync(hostUrl)).ShouldBe(hostUrl);
        
        using (_currentTenant.Change(_tenantAId))
        {
            _currentTenant.Id.ShouldBe(_tenantAId);
            (await _multiTenantUrlProvider.GetUrlAsync(tenantNameHolderUrl)).ShouldBe("https://TenantA.tiknas.io");
            (await _multiTenantUrlProvider.GetUrlAsync(tenantIdHolderUrl)).ShouldBe("https://"+_tenantAId+".tiknas.io");
            (await _multiTenantUrlProvider.GetUrlAsync(tenantHolderUrl)).ShouldBe("https://TenantA.tiknas.io");
            (await _multiTenantUrlProvider.GetUrlAsync(hostUrl)).ShouldBe(hostUrl);
        }
    }
    
}