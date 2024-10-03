using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using Polly.Extensions.Http;
using Tiknas.Cli.Auth;
using Tiknas.Cli.Http;
using Tiknas.Cli.ProjectBuilding;
using Tiknas.DependencyInjection;
using Tiknas.Json;
using Tiknas.Threading;

namespace Tiknas.Cli.Licensing;

public class TiknasIoApiKeyService : IApiKeyService, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }
    protected IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }
    protected ICancellationTokenProvider CancellationTokenProvider { get; }

    private readonly ILogger<TiknasIoApiKeyService> _logger;
    private DeveloperApiKeyResult _apiKeyResult = null;
    private readonly CliHttpClientFactory _cliHttpClientFactory;

    public TiknasIoApiKeyService(
        IJsonSerializer jsonSerializer,
        ICancellationTokenProvider cancellationTokenProvider,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
        ILogger<TiknasIoApiKeyService> logger,
        CliHttpClientFactory cliHttpClientFactory)
    {
        JsonSerializer = jsonSerializer;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        _logger = logger;
        _cliHttpClientFactory = cliHttpClientFactory;
        CancellationTokenProvider = cancellationTokenProvider;
    }

    public async Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync(bool invalidateCache = false)
    {
        if (!AuthService.IsLoggedIn())
        {
            return null;
        }

        if (invalidateCache)
        {
            _apiKeyResult = null;
        }

        if (_apiKeyResult != null)
        {
            return _apiKeyResult;
        }

        var url = $"{CliUrls.WwwTiknasIo}api/license/api-key";
        var client = _cliHttpClientFactory.CreateClient();

        using (var response = await client.GetHttpResponseMessageWithRetryAsync(url, CancellationTokenProvider.Token, _logger))
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR: Remote server returns '{response.StatusCode}'");
            }

            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(response);

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DeveloperApiKeyResult>(responseContent);
        }

    }
}
