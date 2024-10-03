using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.Extensions.Logging;
using Tiknas.Cli.Commands;
using Tiknas.Cli.Http;
using Tiknas.Cli.ProjectBuilding;
using Tiknas.DependencyInjection;
using Tiknas.IdentityModel;
using Tiknas.Json;
using Tiknas.Threading;

namespace Tiknas.Cli.Auth;

public class AuthService : IAuthService, ITransientDependency
{
    protected IIdentityModelAuthenticationService AuthenticationService { get; }
    protected ILogger<AuthService> Logger { get; }
    protected CliHttpClientFactory CliHttpClientFactory { get; }
    public RemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }
    public IJsonSerializer JsonSerializer { get; }
    public ICancellationTokenProvider CancellationTokenProvider { get; }

    public AuthService(
        IIdentityModelAuthenticationService authenticationService,
        ILogger<AuthService> logger,
        ICancellationTokenProvider cancellationTokenProvider,
        CliHttpClientFactory cliHttpClientFactory,
        RemoteServiceExceptionHandler remoteServiceExceptionHandler,
        IJsonSerializer jsonSerializer
    )
    {
        AuthenticationService = authenticationService;
        Logger = logger;
        CancellationTokenProvider = cancellationTokenProvider;
        CliHttpClientFactory = cliHttpClientFactory;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        JsonSerializer = jsonSerializer;
    }

    public async Task<LoginInfo> GetLoginInfoAsync()
    {
        if (!IsLoggedIn())
        {
            return null;
        }

        var url = $"{CliUrls.WwwTiknasIo}api/license/login-info";

        var client = CliHttpClientFactory.CreateClient();

        using (var response = await client.GetHttpResponseMessageWithRetryAsync(url, CancellationTokenProvider.Token, Logger))
        {
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"Remote server returns '{response.StatusCode}'");
                return null;
            }

            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(response);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginInfo>(responseContent);
        }
    }

    public async Task LoginAsync(string userName, string password, string organizationName = null)
    {
        var configuration = new IdentityClientConfiguration(
            CliUrls.AccountTiknasIo,
            "tiknasio offline_access",
            "tiknas-cli",
            null,
            OidcConstants.GrantTypes.Password,
            userName,
            password
        );

        if (!organizationName.IsNullOrWhiteSpace())
        {
            configuration["[o]tiknas-organization-name"] = organizationName;
        }

        var accessToken = await AuthenticationService.GetAccessTokenAsync(configuration);

        File.WriteAllText(CliPaths.AccessToken, accessToken, Encoding.UTF8);
    }

    public async Task DeviceLoginAsync()
    {
        var configuration = new IdentityClientConfiguration(
            CliUrls.AccountTiknasIo,
            "tiknasio offline_access",
            "tiknas-cli",
            null,
            OidcConstants.GrantTypes.DeviceCode
        );

        var accessToken = await AuthenticationService.GetAccessTokenAsync(configuration);

        File.WriteAllText(CliPaths.AccessToken, accessToken, Encoding.UTF8);
    }

    public async Task LogoutAsync()
    {
        string accessToken = null;
        if (File.Exists(CliPaths.AccessToken))
        {
            accessToken = File.ReadAllText(CliPaths.AccessToken);
            File.Delete(CliPaths.AccessToken);
        }

        if (File.Exists(CliPaths.Lic))
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await LogoutAsync(accessToken);
            }

            File.Delete(CliPaths.Lic);
        }
    }

    public async Task<bool> CheckMultipleOrganizationsAsync(string username)
    {
        var url = $"{CliUrls.WwwTiknasIo}api/license/check-multiple-organizations?username={username}";

        var client = CliHttpClientFactory.CreateClient();

        using (var response = await client.GetHttpResponseMessageWithRetryAsync(url, CancellationTokenProvider.Token, Logger))
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR: Remote server returns '{response.StatusCode}'");
            }

            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(response);

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(responseContent);
        }
    }

    private async Task LogoutAsync(string accessToken)
    {
        try
        {
            var client = CliHttpClientFactory.CreateClient();
            var content = new StringContent(
                JsonSerializer.Serialize(new { token = accessToken }),
                Encoding.UTF8, "application/json"
            );

            using (var response = await client.PostAsync(CliConsts.LogoutUrl, content, CancellationTokenProvider.Token))
            {
                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogWarning(
                        $"Cannot logout from remote service! Response: {response.StatusCode}-{response.ReasonPhrase}"
                    );
                }
            }
        }
        catch (Exception e)
        {
            Logger.LogWarning($"Error occured while logging out from remote service. {e.Message}");
        }
    }

    public static bool IsLoggedIn()
    {
        return File.Exists(CliPaths.AccessToken);
    }
}
