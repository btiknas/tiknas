using System;
using System.Security.Claims;

namespace Tiknas.Security.Claims;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal Principal { get; }

    IDisposable Change(ClaimsPrincipal principal);
}
