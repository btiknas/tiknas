using Tiknas.Data;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.EntityFrameworkCore.GlobalFilters;

public interface ITiknasEfCoreDbFunctionContext
{
    ITiknasLazyServiceProvider LazyServiceProvider { get; set; }

    ICurrentTenant CurrentTenant { get; }

    IDataFilter DataFilter { get; }

    string GetCompiledQueryCacheKey();
}
