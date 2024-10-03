using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Configuration;

public interface ICurrentApplicationConfigurationCacheResetService
{
    Task ResetAsync();
}
