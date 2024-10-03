using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.MultiTenancy;
using Tiknas.MultiTenancy.ConfigurationStore;
using Xunit;

namespace Tiknas.AspNetCore.MultiTenancy;

public class AspNetCoreMultiTenancy_WithDomainResolver_Tests : AspNetCoreMultiTenancyTestBase
{
    private readonly Guid _testTenantId = Guid.NewGuid();
    private readonly string _testTenantName = "acme";
    private readonly string _testTenantNormalizedName = "ACME";

    private readonly TiknasAspNetCoreMultiTenancyOptions _options;

    public AspNetCoreMultiTenancy_WithDomainResolver_Tests()
    {
        _options = ServiceProvider.GetRequiredService<IOptions<TiknasAspNetCoreMultiTenancyOptions>>().Value;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TiknasDefaultTenantStoreOptions>(options =>
        {
            options.Tenants = new[]
            {
                new TenantConfiguration(_testTenantId, _testTenantName, _testTenantNormalizedName)
            };
        });

        services.Configure<TiknasTenantResolveOptions>(options =>
        {
            options.AddDomainTenantResolver("{0}.tiknas.io:8080");
        });

        base.ConfigureServices(services);
    }

    [Fact]
    public async Task Should_Use_Host_If_Tenant_Is_Not_Specified()
    {
        var result = await GetResponseAsObjectAsync<Dictionary<string, string>>("http://tiknas.io:8080");
        result["TenantId"].ShouldBe("");
    }

    [Fact]
    public async Task Should_Use_Domain_If_Specified()
    {
        var result = await GetResponseAsObjectAsync<Dictionary<string, string>>("http://acme.tiknas.io:8080");
        result["TenantId"].ShouldBe(_testTenantId.ToString());
    }

    [Fact]
    public async Task Should_Use_Domain_As_First_Priority_If_Specified()
    {
        Client.DefaultRequestHeaders.Add(_options.TenantKey, Guid.NewGuid().ToString());

        var result = await GetResponseAsObjectAsync<Dictionary<string, string>>("http://acme.tiknas.io:8080");
        result["TenantId"].ShouldBe(_testTenantId.ToString());
    }
}
