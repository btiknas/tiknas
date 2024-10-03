using System.Threading.Tasks;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

namespace Tiknas.AspNetCore.Mvc.Client;

public interface ICachedApplicationConfigurationClient
{
    Task<ApplicationConfigurationDto> GetAsync();

    ApplicationConfigurationDto Get();
}
