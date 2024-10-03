using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Components.Web.Security;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Components.MauiBlazor;

public class MauiBlazorCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ITransientDependency
{
    private TiknasComponentsClaimsCache ClaimsCache { get; }

    public MauiBlazorCurrentPrincipalAccessor(
        IClientScopeServiceProviderAccessor clientScopeServiceProviderAccessor)
    {
        ClaimsCache = clientScopeServiceProviderAccessor.ServiceProvider.GetRequiredService<TiknasComponentsClaimsCache>();
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return ClaimsCache.Principal;
    }
}