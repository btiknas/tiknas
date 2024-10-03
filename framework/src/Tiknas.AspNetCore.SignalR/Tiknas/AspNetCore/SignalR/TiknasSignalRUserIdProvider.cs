using Microsoft.AspNetCore.SignalR;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;
using Tiknas.Users;

namespace Tiknas.AspNetCore.SignalR;

public class TiknasSignalRUserIdProvider : IUserIdProvider, ITransientDependency
{
    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

    private readonly ICurrentUser _currentUser;

    public TiknasSignalRUserIdProvider(ICurrentPrincipalAccessor currentPrincipalAccessor, ICurrentUser currentUser)
    {
        _currentPrincipalAccessor = currentPrincipalAccessor;
        _currentUser = currentUser;
    }

    public virtual string? GetUserId(HubConnectionContext connection)
    {
        using (_currentPrincipalAccessor.Change(connection.User))
        {
            return _currentUser.Id?.ToString();
        }
    }
}
