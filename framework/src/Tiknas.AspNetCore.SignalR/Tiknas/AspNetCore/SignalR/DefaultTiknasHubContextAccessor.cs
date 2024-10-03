using System;
using System.Threading;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.SignalR;

public class DefaultTiknasHubContextAccessor : ITiknasHubContextAccessor, ISingletonDependency
{
    public TiknasHubContext Context => _currentHubContext.Value!;

    private readonly AsyncLocal<TiknasHubContext> _currentHubContext = new AsyncLocal<TiknasHubContext>();

    public virtual IDisposable Change(TiknasHubContext context)
    {
        var parent = Context;
        _currentHubContext.Value = context;
        return new DisposeAction(() =>
        {
            _currentHubContext.Value = parent;
        });
    }
}
