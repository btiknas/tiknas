using System.Threading.Tasks;

namespace Tiknas.Cli.Licensing;

public interface IApiKeyService
{
    Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync(bool invalidateCache = false);
}
