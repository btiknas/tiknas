using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.DistributedLocking;

public class DistributedLockKeyNormalizer : IDistributedLockKeyNormalizer, ITransientDependency
{
    protected TiknasDistributedLockOptions Options { get; }
    
    public DistributedLockKeyNormalizer(IOptions<TiknasDistributedLockOptions> options)
    {
        Options = options.Value;
    }
    
    public virtual string NormalizeKey(string name)
    {
        return $"{Options.KeyPrefix}{name}";
    }
}