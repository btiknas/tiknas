using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.DependencyInjection;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Security.Claims;

public class TiknasClaimsPrincipalFactory_Tests : TiknasIntegratedTest<TiknasSecurityTestModule>
{
    private readonly ITiknasClaimsPrincipalFactory _tiknasClaimsPrincipalFactory;
    private static string TestAuthenticationType => "Identity.Application";

    public TiknasClaimsPrincipalFactory_Tests()
    {
        _tiknasClaimsPrincipalFactory = GetRequiredService<ITiknasClaimsPrincipalFactory>();

    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var claimsPrincipal = await _tiknasClaimsPrincipalFactory.CreateAsync();
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@tiknas.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@tiknas.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task Create_With_Exists_ClaimsPrincipal()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TestAuthenticationType, ClaimTypes.Name, ClaimTypes.Role));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Name, "123"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Role, "admin"));

        await _tiknasClaimsPrincipalFactory.CreateAsync(claimsPrincipal);
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "123");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@tiknas.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@tiknas.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task DynamicCreateAsync()
    {
        var claimsPrincipal = await _tiknasClaimsPrincipalFactory.CreateDynamicAsync();
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "manager");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@tiknas.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@tiknas.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task DynamicCreate_With_Exists_ClaimsPrincipal()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TestAuthenticationType, ClaimTypes.Name, ClaimTypes.Role));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Name, "123"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Role, "123"));

        await _tiknasClaimsPrincipalFactory.CreateDynamicAsync(claimsPrincipal);
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Name && x.Value == "123");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Role && x.Value == "123");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "manager");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@tiknas.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@tiknas.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    class TestTiknasClaimsPrincipalContributor : ITiknasClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin@tiknas.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test2TiknasClaimsPrincipalContributor : ITiknasClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin2@tiknas.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test3TiknasClaimsPrincipalContributor : ITiknasClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Version, "2.0"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class TestTiknasDynamicClaimsPrincipalContributor : ITiknasDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin@tiknas.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test2TiknasDynamicClaimsPrincipalContributor : ITiknasDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin2@tiknas.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test3TiknasDynamicClaimsPrincipalContributor : ITiknasDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Version, "2.0"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test4TiknasDynamicClaimsPrincipalContributor : ITiknasDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(TiknasClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Name, "admin"));
            claimsIdentity.RemoveAll(ClaimTypes.Role);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "manager"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }
}
