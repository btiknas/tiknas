using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Tiknas.DependencyInjection;

namespace Tiknas.Authorization;

public interface ITiknasAuthorizationService : IAuthorizationService, IServiceProviderAccessor
{
    ClaimsPrincipal CurrentPrincipal { get; }
}
