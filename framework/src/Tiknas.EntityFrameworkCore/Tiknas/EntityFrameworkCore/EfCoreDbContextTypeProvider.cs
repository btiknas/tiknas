using System;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.EntityFrameworkCore;

public class EfCoreDbContextTypeProvider : IEfCoreDbContextTypeProvider, ITransientDependency
{
    private readonly TiknasDbContextOptions _options;
    private readonly ICurrentTenant _currentTenant;

    public EfCoreDbContextTypeProvider(IOptions<TiknasDbContextOptions> options, ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
        _options = options.Value;
    }

    public virtual Type GetDbContextType(Type dbContextType)
    {
        return _options.GetReplacedTypeOrSelf(dbContextType, _currentTenant.GetMultiTenancySide());
    }
}
