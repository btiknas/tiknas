using System;
using System.Threading;

namespace Tiknas.EntityFrameworkCore.GlobalFilters;

public class TiknasEfCoreCurrentDbContext
{
    private readonly AsyncLocal<ITiknasEfCoreDbFunctionContext?> _current = new AsyncLocal<ITiknasEfCoreDbFunctionContext?>();

    public ITiknasEfCoreDbFunctionContext? Context => _current.Value;

    public IDisposable Use(ITiknasEfCoreDbFunctionContext? context)
    {
        var previousValue = Context;
        _current.Value = context;
        return new DisposeAction(() =>
        {
            _current.Value = previousValue;
        });
    }
}
