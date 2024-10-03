using System.Threading.Tasks;

namespace Tiknas.Security.Claims;

public interface ITiknasClaimsPrincipalContributor
{
    Task ContributeAsync(TiknasClaimsPrincipalContributorContext context);
}
