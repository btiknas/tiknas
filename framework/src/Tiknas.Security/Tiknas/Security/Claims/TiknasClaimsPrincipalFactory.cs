using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Security.Claims;

public class TiknasClaimsPrincipalFactory : ITiknasClaimsPrincipalFactory, ITransientDependency
{
    public static string AuthenticationType => "Tiknas.Application";

    protected IServiceProvider ServiceProvider { get; }
    protected TiknasClaimsPrincipalFactoryOptions Options { get; }

    public TiknasClaimsPrincipalFactory(
        IServiceProvider serviceProvider,
        IOptions<TiknasClaimsPrincipalFactoryOptions> tiknasClaimOptions)
    {
        ServiceProvider = serviceProvider;
        Options = tiknasClaimOptions.Value;
    }

    public virtual async Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal? existsClaimsPrincipal = null)
    {
        return await InternalCreateAsync(Options, existsClaimsPrincipal, false);
    }

    public virtual async Task<ClaimsPrincipal> CreateDynamicAsync(ClaimsPrincipal? existsClaimsPrincipal = null)
    {
        return await InternalCreateAsync(Options, existsClaimsPrincipal, true);
    }

    public virtual async Task<ClaimsPrincipal> InternalCreateAsync(TiknasClaimsPrincipalFactoryOptions options, ClaimsPrincipal? existsClaimsPrincipal = null, bool isDynamic = false)
    {
        var claimsPrincipal = existsClaimsPrincipal ?? new ClaimsPrincipal(new ClaimsIdentity(
            AuthenticationType,
            TiknasClaimTypes.UserName,
            TiknasClaimTypes.Role));

        var context = new TiknasClaimsPrincipalContributorContext(claimsPrincipal, ServiceProvider);

        if (!isDynamic)
        {
            foreach (var contributorType in options.Contributors)
            {
                var contributor = (ITiknasClaimsPrincipalContributor)ServiceProvider.GetRequiredService(contributorType);
                await contributor.ContributeAsync(context);
            }
        }
        else
        {
            foreach (var contributorType in options.DynamicContributors)
            {
                var contributor = (ITiknasDynamicClaimsPrincipalContributor)ServiceProvider.GetRequiredService(contributorType);
                await contributor.ContributeAsync(context);
            }
        }

        return context.ClaimsPrincipal;
    }
}
