using System.Security.Claims;
using System.Threading.Tasks;

namespace Tiknas.Security.Claims;

public interface ITiknasClaimsPrincipalFactory
{
    Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal? existsClaimsPrincipal = null);

    Task<ClaimsPrincipal> CreateDynamicAsync(ClaimsPrincipal? existsClaimsPrincipal = null);
}
