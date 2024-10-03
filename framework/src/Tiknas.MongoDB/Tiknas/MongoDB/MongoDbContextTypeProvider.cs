using System;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.MongoDB;

public class MongoDbContextTypeProvider : IMongoDbContextTypeProvider, ITransientDependency
{
    private readonly TiknasMongoDbContextOptions _options;
    private readonly ICurrentTenant _currentTenant;

    public MongoDbContextTypeProvider(IOptions<TiknasMongoDbContextOptions> options, ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
        _options = options.Value;
    }

    public virtual Type GetDbContextType(Type dbContextType)
    {
        return _options.GetReplacedTypeOrSelf(dbContextType, _currentTenant.GetMultiTenancySide());
    }
}
