using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.Authorization.Permissions;

public class NullPermissionStore : IPermissionStore, ISingletonDependency
{
    public ILogger<NullPermissionStore> Logger { get; set; }

    public NullPermissionStore()
    {
        Logger = NullLogger<NullPermissionStore>.Instance;
    }

    public Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
    {
        return TaskCache.FalseResult;
    }

    public Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names, string providerName, string providerKey)
    {
        return Task.FromResult(new MultiplePermissionGrantResult(names, PermissionGrantResult.Prohibited));
    }
}
