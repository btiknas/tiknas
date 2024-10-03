using System.Threading.Tasks;

namespace Tiknas.Cli.ServiceProxying;

public interface IServiceProxyGenerator
{
    Task GenerateProxyAsync(GenerateProxyArgs args);
}
