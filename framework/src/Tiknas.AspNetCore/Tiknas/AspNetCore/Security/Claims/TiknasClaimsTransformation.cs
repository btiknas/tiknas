using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Tiknas.AspNetCore.Security.Claims;

public class TiknasClaimsTransformation : IClaimsTransformation
{
    protected IOptions<TiknasClaimsMapOptions> TiknasClaimsMapOptions { get; }

    public TiknasClaimsTransformation(IOptions<TiknasClaimsMapOptions> tiknasClaimsMapOptions)
    {
        TiknasClaimsMapOptions = tiknasClaimsMapOptions;
    }

    public virtual Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var mapClaims = principal.Claims.Where(claim => TiknasClaimsMapOptions.Value.Maps.Keys.Contains(claim.Type));

        principal.AddIdentity(new ClaimsIdentity(mapClaims.Select(
                    claim => new Claim(
                        TiknasClaimsMapOptions.Value.Maps[claim.Type](),
                        claim.Value,
                        claim.ValueType,
                        claim.Issuer
                    )
                )
            )
        );

        return Task.FromResult(principal);
    }
}
