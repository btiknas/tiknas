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
            options.Applications["MVC"].RootUrl = "https://{{tenantName}}.tiknas.io";
            options.Applications["MVC"].Urls["PasswordReset"] = "account/reset-password";
            options.RedirectAllowedUrls.AddRange(new List<string>()
            {
                "https://wwww.volosoft.com",
                "https://wwww.aspnetzero.com",
                "https://{{tenantName}}.tiknas.io",
                "https://{{tenantId}}.tiknas.io",
                "https://*.demo.mytiknas.io"
            });

            options.Applications["BLAZOR"].RootUrl = "https://{{tenantId}}.tiknas.io";
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
            url.ShouldBe("https://tiknas.io");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://tiknas.io/account/reset-password");
        }

        using (_currentTenant.Change(Guid.NewGuid(), "community"))
        {
            var url = await _appUrlProvider.GetUrlAsync("MVC");
            url.ShouldBe("https://community.tiknas.io");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://community.tiknas.io/account/reset-password");
        }

        using (_currentTenant.Change(_tenantAId))
        {
            var url = await _appUrlProvider.GetUrlAsync("BLAZOR");
            url.ShouldBe($"https://{_tenantAId}.tiknas.io");

            url = await _appUrlProvider.GetUrlAsync("BLAZOR", "PasswordReset");
            url.ShouldBe($"https://{_tenantAId}.tiknas.io/account/reset-password");
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
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.volosoft.com")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.demo.mytiknas.io")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://demo.mytiknas.io")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://api.demo.mytiknas.io")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://test.api.demo.mytiknas.io")).ShouldBeTrue();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://volosoft.com/demo.mytiknas.io")).ShouldBeFalse();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.mytiknas.io")).ShouldBeFalse();

        using (_currentTenant.Change(null))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://tiknas.io")).ShouldBeTrue();
        }

        using (_currentTenant.Change(_tenantAId, "community"))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community.tiknas.io")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community2.tiknas.io")).ShouldBeFalse();
        }

        using (_currentTenant.Change(_tenantAId))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{_tenantAId}.tiknas.io")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{Guid.NewGuid()}.tiknas.io")).ShouldBeFalse();
        }
    }
}
