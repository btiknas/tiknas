using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Localization.External;

public interface IExternalLocalizationStore
{
    LocalizationResourceBase? GetResourceOrNull([NotNull] string resourceName);
    
    Task<LocalizationResourceBase?> GetResourceOrNullAsync([NotNull] string resourceName);
    
    Task<string[]> GetResourceNamesAsync();
    
    Task<LocalizationResourceBase[]> GetResourcesAsync();
}