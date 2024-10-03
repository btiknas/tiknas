using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.SimpleStateChecking;
using Tiknas.Users;

namespace Tiknas.Authorization.Permissions;

public class RequireAuthenticatedSimpleStateChecker<TState> : ISimpleStateChecker<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<TState> context)
    {
        return Task.FromResult(context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated);
    }
}
