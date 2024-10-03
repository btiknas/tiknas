using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Middleware;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Security.Claims;

public class TiknasDynamicClaimsMiddleware : TiknasMiddlewareBase, ITransientDependency
{
    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            if (context.RequestServices.GetRequiredService<IOptions<TiknasClaimsPrincipalFactoryOptions>>().Value.IsDynamicClaimsEnabled)
            {
                var authenticateResultFeature = context.Features.Get<IAuthenticateResultFeature>();
                var authenticationType = authenticateResultFeature?.AuthenticateResult?.Ticket?.AuthenticationScheme ?? context.User.Identity.AuthenticationType;

                if (authenticateResultFeature != null && !authenticationType.IsNullOrWhiteSpace())
                {
                    var tiknasClaimsPrincipalFactory = context.RequestServices.GetRequiredService<ITiknasClaimsPrincipalFactory>();
                    var user = await tiknasClaimsPrincipalFactory.CreateDynamicAsync(context.User);

                    authenticateResultFeature.AuthenticateResult = AuthenticateResult.Success(new AuthenticationTicket(user, authenticationType));
                    var httpAuthenticationFeature = context.Features.Get<IHttpAuthenticationFeature>();
                    if (httpAuthenticationFeature != null)
                    {
                        httpAuthenticationFeature.User = authenticateResultFeature.AuthenticateResult.Principal;
                    }
                }

                if (context.User.Identity?.IsAuthenticated == false)
                {
                    var authenticationSchemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
                    if (!authenticationType.IsNullOrWhiteSpace())
                    {
                        var authenticationScheme = await authenticationSchemeProvider.GetSchemeAsync(authenticationType);
                        if (authenticationScheme != null && typeof(IAuthenticationSignOutHandler).IsAssignableFrom(authenticationScheme.HandlerType))
                        {
                            await context.SignOutAsync(authenticationScheme.Name);
                        }
                    }
                }
            }
        }

        await next(context);
    }
}
