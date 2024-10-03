using System.Threading.Tasks;

namespace Tiknas.Modularity;

public interface IPostConfigureServices
{
    Task PostConfigureServicesAsync(ServiceConfigurationContext context);

    void PostConfigureServices(ServiceConfigurationContext context);
}
