using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.DependencyInjection;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Security.Claims;

[DisableConventionalRegistration]
class TestTiknasDynamicClaimsPrincipalContributor : TiknasDynamicClaimsPrincipalContributorBase
{
    private readonly List<TiknasDynamicClaim> _claims;

    public TestTiknasDynamicClaimsPrincipalContributor(List<TiknasDynamicClaim> claims)
    {
        _claims = claims;
    }

    public async override Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
    {
        var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
        Check.NotNull(identity, nameof(identity));

        await AddDynamicClaimsAsync(context, identity, _claims);
    }
}

public class TiknasDynamicClaimsPrincipalContributorBase_Tests : TiknasIntegratedTest<TiknasSecurityTestModule>
{
    private readonly TestTiknasDynamicClaimsPrincipalContributor _dynamicClaimsPrincipalContributorBase;

    private readonly TiknasDynamicClaimCacheItem _dynamicClaims;

    public TiknasDynamicClaimsPrincipalContributorBase_Tests()
    {
        _dynamicClaims = new TiknasDynamicClaimCacheItem(new List<TiknasDynamicClaim>()
        {
            new TiknasDynamicClaim("preferred_username", "test-preferred_username"),
            new TiknasDynamicClaim(ClaimTypes.GivenName, "test-given_name"),
            new TiknasDynamicClaim("family_name", "test-family_name"),
            new TiknasDynamicClaim("role", "test-role1"),
            new TiknasDynamicClaim("roles", "test-role2"),
            new TiknasDynamicClaim(ClaimTypes.Role, "test-role3"),
            new TiknasDynamicClaim("email", "test-email"),
            new TiknasDynamicClaim(TiknasClaimTypes.EmailVerified, "test-email-verified"),
            new TiknasDynamicClaim(TiknasClaimTypes.PhoneNumberVerified, null),
        });
        _dynamicClaimsPrincipalContributorBase = new TestTiknasDynamicClaimsPrincipalContributor(_dynamicClaims.Claims);
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task AddDynamicClaimsAsync()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.UserName, "test-source-userName"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.Name, "test-source-name"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.SurName, "test-source-surname"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.Role, "test-source-role1"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.Role, "test-source-role2"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.Email, "test-source-email"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.EmailVerified, "test-source-emailVerified"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.PhoneNumber, "test-source-phoneNumber"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(TiknasClaimTypes.PhoneNumberVerified, "test-source-phoneNumberVerified"));
        claimsPrincipal.Identities.First().AddClaim(new Claim("my-claim", "test-source-my-claim"));

        await _dynamicClaimsPrincipalContributorBase.ContributeAsync(new TiknasClaimsPrincipalContributorContext(claimsPrincipal, GetRequiredService<IServiceProvider>()));

        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.UserName && c.Value == "test-preferred_username");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.SurName && c.Value == "test-family_name");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.Name && c.Value == "test-given_name");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.Role && c.Value == "test-role1");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.Role && c.Value == "test-role2");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.Role && c.Value == "test-role3");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.Email && c.Value == "test-email");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.EmailVerified && c.Value == "test-email-verified");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == TiknasClaimTypes.PhoneNumber && c.Value == "test-source-phoneNumber");
        claimsPrincipal.Identities.First().Claims.ShouldNotContain(c => c.Type == TiknasClaimTypes.PhoneNumberVerified);
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == "my-claim" && c.Value == "test-source-my-claim");
    }
}
