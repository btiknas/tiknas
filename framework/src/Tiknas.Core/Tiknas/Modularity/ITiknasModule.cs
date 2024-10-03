using System.Threading.Tasks;

namespace Tiknas.Modularity;

public interface ITiknasModule
{
    Task ConfigureServicesAsync(ServiceConfigurationContext context);

    void ConfigureServices(ServiceConfigurationContext context);
}
