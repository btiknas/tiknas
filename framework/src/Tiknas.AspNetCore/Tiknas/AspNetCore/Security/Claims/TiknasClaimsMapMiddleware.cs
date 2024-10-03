using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Middleware;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Security.Claims;

[Obsolete("Replace with TiknasClaimsTransformation")]
public class TiknasClaimsMapMiddleware : TiknasMiddlewareBase, ITransientDependency
{
    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var currentPrincipalAccessor = context.RequestServices
            .GetRequiredService<ICurrentPrincipalAccessor>();

        var mapOptions = context.RequestServices
            .GetRequiredService<IOptions<TiknasClaimsMapOptions>>().Value;

        var mapClaims = currentPrincipalAccessor
            .Principal
            .Claims
            .Where(claim => mapOptions.Maps.Keys.Contains(claim.Type));

        currentPrincipalAccessor
            .Principal
            .AddIdentity(
                new ClaimsIdentity(
                    mapClaims
                        .Select(
                            claim => new Claim(
                                mapOptions.Maps[claim.Type](),
                                claim.Value,
                                claim.ValueType,
                                claim.Issuer
                            )
                        )
                )
            );

        await next(context);
    }
}
