using System;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Ldap;
/// <summary>
/// docker run --name ldap -d --env LDAP_ORGANISATION="tiknas" --env LDAP_DOMAIN="tiknas.de" --env LDAP_ADMIN_PASSWORD="123qwe" -p 389:389 -p 636:639 osixia/openldap
/// </summary>
public class LdapManager_Tests : TiknasIntegratedTest<TiknasLdapTestModule>
{
    private readonly ILdapManager _ldapManager;

    public LdapManager_Tests()
    {
        _ldapManager = GetRequiredService<ILdapManager>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact(Skip = "Required Ldap environment")]
    public async Task AuthenticateAsync()
    {
        (await _ldapManager.AuthenticateAsync("cn=admin,dc=tiknas,dc=de", "123qwe")).ShouldBe(true);
        (await _ldapManager.AuthenticateAsync("cn=tiknas,dc=tiknas,dc=de", "123123")).ShouldBe(false);
        (await _ldapManager.AuthenticateAsync("NoExists", "123qwe")).ShouldBe(false);
    }
}
