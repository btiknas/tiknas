using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.Settings;
using Xunit;

namespace Tiknas.MultiTenancy;

public class CurrentUserTenantResolveContributor_Tests : MultiTenancyTestBase
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Add(new TestTenantResolveContributor());
        });
    }

    [Fact]
    public void CurrentUserTenantResolveContributor_Should_Add_First()
    {
        var options = GetRequiredService<IOptions<TiknasTenantResolveOptions>>().Value;
        options.TenantResolvers.First().GetType().ShouldBe(typeof(CurrentUserTenantResolveContributor));
    }

    class TestTenantResolveContributor : ITenantResolveContributor
    {
        public string Name { get; }

        public Task ResolveAsync(ITenantResolveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
