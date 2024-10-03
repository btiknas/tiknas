using System.Threading.Tasks;

namespace Tiknas.Security.Claims;

public interface ITiknasDynamicClaimsPrincipalContributor
{
    Task ContributeAsync(TiknasClaimsPrincipalContributorContext context);
}
