using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Data;

public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
{
    protected TiknasDbConnectionOptions Options { get; }

    public DefaultConnectionStringResolver(
        IOptionsMonitor<TiknasDbConnectionOptions> options)
    {
        Options = options.CurrentValue;
    }

    [Obsolete("Use ResolveAsync method.")]
    public virtual string Resolve(string? connectionStringName = null)
    {
        return ResolveInternal(connectionStringName)!;
    }

    public virtual Task<string> ResolveAsync(string? connectionStringName = null)
    {
        return Task.FromResult(ResolveInternal(connectionStringName))!;
    }

    private string? ResolveInternal(string? connectionStringName)
    {
        if (connectionStringName == null)
        {
            return Options.ConnectionStrings.Default;
        }

        var connectionString = Options.GetConnectionStringOrNull(connectionStringName);

        if (!connectionString.IsNullOrEmpty())
        {
            return connectionString;
        }

        return null;
    }
}
