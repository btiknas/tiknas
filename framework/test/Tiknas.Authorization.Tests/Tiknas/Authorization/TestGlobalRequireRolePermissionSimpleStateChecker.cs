using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Authorization.Permissions;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;
using Tiknas.SimpleStateChecking;

namespace Tiknas.Authorization;

public class TestGlobalRequireRolePermissionSimpleStateChecker : ISimpleStateChecker<PermissionDefinition>, ITransientDependency
{
    public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<PermissionDefinition> context)
    {
        var currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        return Task.FromResult(currentPrincipalAccessor.Principal != null && currentPrincipalAccessor.Principal.IsInRole("admin"));
    }
}
