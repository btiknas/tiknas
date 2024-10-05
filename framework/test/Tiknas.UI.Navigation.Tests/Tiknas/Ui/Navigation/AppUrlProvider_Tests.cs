using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.MultiTenancy;
using Tiknas.MultiTenancy.ConfigurationStore;
using Tiknas.TestBase;
using Tiknas.UI.Navigation.Urls;
using Xunit;

namespace Tiknas.UI.Navigation;

public class AppUrlProvider_Tests : TiknasIntegratedTest<TiknasUiNavigationTestModule>
{
    private readonly IAppUrlProvider _appUrlProvider;
    private readonly ICurrentTenant _currentTenant;

    private readonly Guid _tenantAId = Guid.NewGuid();

    public AppUrlProvider_Tests()
    {
        _appUrlProvider = ServiceProvider.GetRequiredService<AppUrlProvider>();
        _currentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = "https://{{tenantName}}.tiknas.de";
            options.Applications["MVC"].Urls["PasswordReset"] = "account/reset-password";
            options.RedirectAllowedUrls.AddRange(new List<string>()
            {
                "https://wwww.tiknassoft.com",
                "https://wwww.aspnetzero.com",
                "https://{{tenantName}}.tiknas.de",
                "https://{{tenantId}}.tiknas.de",
                "https://*.demo.mytiknas.de"
            });

            options.Applications["BLAZOR"].RootUrl = "https://{{tenantId}}.tiknas.de";
            options.Applications["BLAZOR"].Urls["PasswordReset"] = "account/reset-password";
        });

        services.Configure<TiknasDefaultTenantStoreOptions>(options =>
        {
            options.Tenants = new TenantConfiguration[]
            {
                new(_tenantAId, "community")
            };
        });
    }

    [Fact]
    public async Task GetUrlAsync()
    {
        using (_currentTenant.Change(null))
        {
            var url = await _appUrlProvider.GetUrlAsync("MVC");
            url.ShouldBe("https://tiknas.de");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://tiknas.de/account/reset-password");
        }

        using (_currentTenant.Change(Guid.NewGuid(), "community"))
        {
            var url = await _appUrlProvider.GetUrlAsync("MVC");
            url.ShouldBe("https://community.tiknas.de");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://community.tiknas.de/account/reset-password");
        }

        using (_currentTenant.Change(_tenantAId))
        {
            var url = await _appUrlProvider.GetUrlAsync("BLAZOR");
            url.ShouldBe($"https://{_tenantAId}.tiknas.de");

            url = await _appUrlProvider.GetUrlAsync("BLAZOR", "PasswordReset");
            url.ShouldBe($"https://{_tenantAId}.tiknas.de/account/reset-password");
        }

        await Assert.ThrowsAsync<TiknasException>(async () =>
        {
            await _appUrlProvider.GetUrlAsync("ANGULAR");
        });
    }

    [Fact]
    public async Task GetUrlOrNullAsync()
    {
        (await _appUrlProvider.GetUrlOrNullAsync("ANGULAR")).ShouldBeNull();
    }

    [Fact]
    public async Task IsRedirectAllowedUrlAsync()
    {
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.tiknassoft.com")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.demo.mytiknas.de")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://demo.mytiknas.de")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://api.demo.mytiknas.de")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://test.api.demo.mytiknas.de")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://tiknassoft.com/demo.mytiknas.de")).ShouldBeFalse();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.mytiknas.de")).ShouldBeFalse();

        using (_currentTenant.Change(null))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://tiknas.de")).ShouldBeTrue();
        }

        using (_currentTenant.Change(_tenantAId, "community"))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community.tiknas.de")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community2.tiknas.de")).ShouldBeFalse();
        }

        using (_currentTenant.Change(_tenantAId))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{_tenantAId}.tiknas.de")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{Guid.NewGuid()}.tiknas.de")).ShouldBeFalse();
        }
    }
}
