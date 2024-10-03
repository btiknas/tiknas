using Microsoft.Extensions.Localization;
using Shouldly;
using Tiknas.Ldap.Localization;
using Tiknas.Localization;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Ldap;

public class LdapResource_Tests : TiknasIntegratedTest<TiknasLdapTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public void LdapResource_Test()
    {
        using (CultureHelper.Use("en"))
        {
            GetRequiredService<IStringLocalizer<LdapResource>>()["DisplayName:Tiknas.Ldap.ServerHost"].Value.ShouldBe("Server host");
        }

        using (CultureHelper.Use("tr"))
        {
            GetRequiredService<IStringLocalizer<LdapResource>>()["DisplayName:Tiknas.Ldap.ServerHost"].Value.ShouldBe("Sunucu Ana Bilgisayarı");
        }
    }
}
