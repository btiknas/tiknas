using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.Json;
using Tiknas.Cli.Http;
using Tiknas.Cli.ProjectBuilding.Building;
using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.Cli.ProjectBuilding;

public class ModuleInfoProvider : IModuleInfoProvider, ITransientDependency
{
    public IJsonSerializer JsonSerializer { get; }
    public ICancellationTokenProvider CancellationTokenProvider { get; }
    public IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }

    private readonly CliHttpClientFactory _cliHttpClientFactory;

    public ModuleInfoProvider(
        IJsonSerializer jsonSerializer,
        ICancellationTokenProvider cancellationTokenProvider,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
        CliHttpClientFactory cliHttpClientFactory)
    {
        JsonSerializer = jsonSerializer;
        CancellationTokenProvider = cancellationTokenProvider;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        _cliHttpClientFactory = cliHttpClientFactory;
    }

    public async Task<ModuleInfo> GetAsync(string name)
    {
        var moduleList = await GetModuleListInternalAsync();

        var module = moduleList.FirstOrDefault(m => m.Name == name);

        if (module == null)
        {
            throw new Exception("Module not found!");
        }

        return module;
    }

    public async Task<List<ModuleInfo>> GetModuleListAsync()
    {
        return await GetModuleListInternalAsync();
    }

    private async Task<List<ModuleInfo>> GetModuleListInternalAsync()
    {
        var client = _cliHttpClientFactory.CreateClient();

        using (var responseMessage = await client.GetAsync(
            $"{CliUrls.WwwTiknasIo}api/download/modules/",
            CancellationTokenProvider.Token
        ))
        {
            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(responseMessage);
            var result = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ModuleInfo>>(result);
        }
    }
}
