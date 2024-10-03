using System.Security.Claims;
using System.Threading;
using Tiknas.DependencyInjection;

namespace Tiknas.Security.Claims;

public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ISingletonDependency
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return (Thread.CurrentPrincipal as ClaimsPrincipal)!;
    }
}
