using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.Security;

public class TiknasComponentsClaimsCache : IScopedDependency
{
    public ClaimsPrincipal Principal { get; private set; } = default!;

    private readonly AuthenticationStateProvider? _authenticationStateProvider;

    public TiknasComponentsClaimsCache(
        IClientScopeServiceProviderAccessor serviceProviderAccessor)
    {
        _authenticationStateProvider = serviceProviderAccessor.ServiceProvider.GetService<AuthenticationStateProvider>();
        if (_authenticationStateProvider != null)
        {
            _authenticationStateProvider.AuthenticationStateChanged += async (task) =>
            {
                Principal = (await task).User;
            };
        }
    }

    public virtual async Task InitializeAsync()
    {
        if (_authenticationStateProvider != null)
        {
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            Principal = authenticationState.User;
        }
    }
}
