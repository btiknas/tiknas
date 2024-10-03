using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Licensing;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands.Services;

public class TiknasNuGetIndexUrlService : ITransientDependency
{
    private readonly IApiKeyService _apiKeyService;
    public ILogger<TiknasNuGetIndexUrlService> Logger { get; set; }

    public TiknasNuGetIndexUrlService(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
        Logger = NullLogger<TiknasNuGetIndexUrlService>.Instance;
    }

    public async Task<string> GetAsync()
    {
        var apiKeyResult = await _apiKeyService.GetApiKeyOrNullAsync();

        if (apiKeyResult == null)
        {
            Logger.LogWarning("You are not signed in! Use the CLI command \"tiknas login <username>\" to sign in, then try again.");
            return null;
        }

        if (!string.IsNullOrWhiteSpace(apiKeyResult.ErrorMessage))
        {
            Logger.LogWarning(apiKeyResult.ErrorMessage);
            return null;
        }

        if (string.IsNullOrEmpty(apiKeyResult.ApiKey))
        {
            Logger.LogError("Couldn't retrieve your NuGet API key! You can re-sign in with the CLI command \"tiknas login <username>\".");
            return null;
        }

        return CliUrls.GetNuGetServiceIndexUrl(apiKeyResult.ApiKey);
    }
}
