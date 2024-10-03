using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Security.Claims;

public abstract class TiknasDynamicClaimsPrincipalContributorBase : ITiknasDynamicClaimsPrincipalContributor, ITransientDependency
{
    public abstract Task ContributeAsync(TiknasClaimsPrincipalContributorContext context);

    protected virtual async Task AddDynamicClaimsAsync(TiknasClaimsPrincipalContributorContext context, ClaimsIdentity identity, List<TiknasDynamicClaim> dynamicClaims)
    {
        var options = context.GetRequiredService<IOptions<TiknasClaimsPrincipalFactoryOptions>>().Value;
        foreach (var map in options.ClaimsMap)
        {
            await MapClaimAsync(identity, dynamicClaims, map.Key, map.Value.ToArray());
        }

        foreach (var claimGroup in dynamicClaims.GroupBy(x => x.Type))
        {
            identity.RemoveAll(claimGroup.First().Type);
            identity.AddClaims(claimGroup.Where(c => c.Value != null).Select(c => new Claim(claimGroup.First().Type, c.Value!)));
        }
    }

    protected virtual Task MapClaimAsync(ClaimsIdentity identity, List<TiknasDynamicClaim> dynamicClaims, string targetClaimType, params string[] sourceClaimTypes)
    {
        var claims = dynamicClaims.Where(c => sourceClaimTypes.Contains(c.Type)).ToList();
        if (claims.IsNullOrEmpty())
        {
            return Task.CompletedTask;
        }

        dynamicClaims.RemoveAll(claims);
        identity.RemoveAll(targetClaimType);
        identity.AddClaims(claims.Where(c => c.Value != null).Select(c => new Claim(targetClaimType, c.Value!)));

        return Task.CompletedTask;;
    }
}
