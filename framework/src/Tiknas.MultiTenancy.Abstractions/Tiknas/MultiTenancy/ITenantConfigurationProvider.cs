using System.Threading.Tasks;

namespace Tiknas.MultiTenancy;

public interface ITenantConfigurationProvider
{
    Task<TenantConfiguration?> GetAsync(bool saveResolveResult = false);
}
