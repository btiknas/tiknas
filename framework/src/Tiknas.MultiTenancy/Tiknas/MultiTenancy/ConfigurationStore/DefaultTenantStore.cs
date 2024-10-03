using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.MultiTenancy.ConfigurationStore;

[Dependency(TryRegister = true)]
public class DefaultTenantStore : ITenantStore, ITransientDependency
{
    private readonly TiknasDefaultTenantStoreOptions _options;

    public DefaultTenantStore(IOptionsMonitor<TiknasDefaultTenantStoreOptions> options)
    {
        _options = options.CurrentValue;
    }

    public Task<TenantConfiguration?> FindAsync(string normalizedName)
    {
        return Task.FromResult(Find(normalizedName));
    }

    public Task<TenantConfiguration?> FindAsync(Guid id)
    {
        return Task.FromResult(Find(id));
    }

    public Task<IReadOnlyList<TenantConfiguration>> GetListAsync(bool includeDetails = false)
    {
        return Task.FromResult<IReadOnlyList<TenantConfiguration>>(_options.Tenants);
    }

    public TenantConfiguration? Find(string normalizedName)
    {
        return _options.Tenants?.FirstOrDefault(t => t.NormalizedName == normalizedName);
    }

    public TenantConfiguration? Find(Guid id)
    {
        return _options.Tenants?.FirstOrDefault(t => t.Id == id);
    }
}
