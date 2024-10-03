using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.BlobStoring;

public class DefaultBlobContainerConfigurationProvider : IBlobContainerConfigurationProvider, ITransientDependency
{
    protected TiknasBlobStoringOptions Options { get; }

    public DefaultBlobContainerConfigurationProvider(IOptions<TiknasBlobStoringOptions> options)
    {
        Options = options.Value;
    }

    public virtual BlobContainerConfiguration Get(string name)
    {
        return Options.Containers.GetConfiguration(name);
    }
}
