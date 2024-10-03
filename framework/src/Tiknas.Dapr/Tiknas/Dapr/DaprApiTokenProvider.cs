using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Dapr;

public class DaprApiTokenProvider : IDaprApiTokenProvider, ISingletonDependency
{
    protected TiknasDaprOptions Options { get; }

    public DaprApiTokenProvider(IOptions<TiknasDaprOptions> options)
    {
        Options = options.Value;
    }

    public virtual string? GetDaprApiToken()
    {
        return Options.DaprApiToken;
    }

    public virtual string? GetAppApiToken()
    {
        return Options.AppApiToken;
    }
}
