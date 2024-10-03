using Microsoft.Extensions.Localization;
using Shouldly;
using Tiknas.Localization;
using Tiknas.MultiTenancy.Localization;
using Xunit;

namespace Tiknas.MultiTenancy;

public class MultiTenancyResource_Tests : MultiTenancyTestBase
{
    [Fact]
    public void MultiTenancyResource_Test()
    {
        var q = GetRequiredService<IStringLocalizer<TiknasMultiTenancyResource>>();
        using (CultureHelper.Use("en"))
        {
            GetRequiredService<IStringLocalizer<TiknasMultiTenancyResource>>()["TenantNotFoundMessage"].Value.ShouldBe("Tenant not found!");
        }

        using (CultureHelper.Use("tr"))
        {
            GetRequiredService<IStringLocalizer<TiknasMultiTenancyResource>>()["TenantNotFoundMessage"].Value.ShouldBe("Kiracı bulunamadı!");
        }
    }
}
