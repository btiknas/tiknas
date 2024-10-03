using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Auth;
using Tiknas.Cli.Http;
using Tiknas.DependencyInjection;
using Tiknas.Http;
using Tiknas.Json;
using Tiknas.Threading;

namespace Tiknas.Cli.ProjectBuilding.Analyticses;

public class CliAnalyticsCollect : ICliAnalyticsCollect, ITransientDependency
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger<CliAnalyticsCollect> _logger;
    private readonly IRemoteServiceExceptionHandler _remoteServiceExceptionHandler;
    private readonly CliHttpClientFactory _cliHttpClientFactory;

    public CliAnalyticsCollect(
        ICancellationTokenProvider cancellationTokenProvider,
        IJsonSerializer jsonSerializer,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
        CliHttpClientFactory cliHttpClientFactory)
    {
        _cancellationTokenProvider = cancellationTokenProvider;
        _jsonSerializer = jsonSerializer;
        _remoteServiceExceptionHandler = remoteServiceExceptionHandler;
        _cliHttpClientFactory = cliHttpClientFactory;
        _logger = NullLogger<CliAnalyticsCollect>.Instance;
    }

    public async Task CollectAsync(CliAnalyticsCollectInputDto input)
    {
        if (input.RandomComputerId.IsNullOrWhiteSpace())
        {
            if (!File.Exists(CliPaths.ComputerId))
            {
                var randomComputerId = Guid.NewGuid().ToString("D");
                input.RandomComputerId = randomComputerId;
                File.WriteAllText(CliPaths.ComputerId, randomComputerId, Encoding.UTF8);
            }
            else
            {
                input.RandomComputerId = File.ReadAllText(CliPaths.ComputerId, Encoding.UTF8);
            }
        }

        var postData = _jsonSerializer.Serialize(input);
        var url = $"{CliUrls.WwwTiknasIo}api/clianalytics/collect";

        try
        {
            var client = _cliHttpClientFactory.CreateClient();

            var responseMessage = await client.PostAsync(
                url,
                new StringContent(postData, Encoding.UTF8, MimeTypes.Application.Json),
                _cancellationTokenProvider.Token
            );

            if (!responseMessage.IsSuccessStatusCode)
            {
                var exceptionMessage = "Remote server returns '" + (int)responseMessage.StatusCode + "-" + responseMessage.ReasonPhrase + "'. ";
                var remoteServiceErrorMessage = await _remoteServiceExceptionHandler.GetTiknasRemoteServiceErrorAsync(responseMessage);

                if (remoteServiceErrorMessage != null)
                {
                    exceptionMessage += remoteServiceErrorMessage;
                }

                _logger.LogInformation(exceptionMessage);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
