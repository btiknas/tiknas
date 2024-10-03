using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.Dapr;
using Tiknas.DependencyInjection;

namespace Tiknas.DistributedLocking.Dapr;

[Dependency(ReplaceServices = true)]
public class DaprTiknasDistributedLock : ITiknasDistributedLock, ITransientDependency
{
    protected ITiknasDaprClientFactory DaprClientFactory { get; }
    protected TiknasDistributedLockDaprOptions DistributedLockDaprOptions { get; }
    protected IDistributedLockKeyNormalizer DistributedLockKeyNormalizer { get; }

    public DaprTiknasDistributedLock(
        ITiknasDaprClientFactory daprClientFactory,
        IOptions<TiknasDistributedLockDaprOptions> distributedLockDaprOptions,
        IDistributedLockKeyNormalizer distributedLockKeyNormalizer)
    {
        DaprClientFactory = daprClientFactory;
        DistributedLockKeyNormalizer = distributedLockKeyNormalizer;
        DistributedLockDaprOptions = distributedLockDaprOptions.Value;
    }

    public async Task<ITiknasDistributedLockHandle?> TryAcquireAsync(
        string name,
        TimeSpan timeout = default,
        CancellationToken cancellationToken = default)
    {
        name = DistributedLockKeyNormalizer.NormalizeKey(name);

        var daprClient = await DaprClientFactory.CreateAsync();
        var lockResponse = await daprClient.Lock(
            DistributedLockDaprOptions.StoreName,
            name,
            DistributedLockDaprOptions.Owner ?? Guid.NewGuid().ToString(),
            (int)DistributedLockDaprOptions.DefaultExpirationTimeout.TotalSeconds,
            cancellationToken);

        if (lockResponse == null || !lockResponse.Success)
        {
            return null;
        }

        return new DaprTiknasDistributedLockHandle(lockResponse);
    }
}
