using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.Cli.Commands;
using Tiknas.Cli.Licensing;
using Tiknas.Cli.ProjectBuilding.Analyticses;
using Tiknas.Cli.ProjectBuilding.Building;
using Tiknas.Cli.ProjectModification;
using Tiknas.DependencyInjection;
using Tiknas.Json;

namespace Tiknas.Cli.ProjectBuilding;

public class NugetPackageProjectBuilder : IProjectBuilder, ITransientDependency
{
    public ILogger<NugetPackageProjectBuilder> Logger { get; set; }
    protected ISourceCodeStore SourceCodeStore { get; }
    protected INugetPackageInfoProvider NugetPackageInfoProvider { get; }
    protected ICliAnalyticsCollect CliAnalyticsCollect { get; }
    protected TiknasCliOptions Options { get; }
    protected IJsonSerializer JsonSerializer { get; }
    protected IApiKeyService ApiKeyService { get; }

    public NugetPackageProjectBuilder(ISourceCodeStore sourceCodeStore,
        INugetPackageInfoProvider nugetPackageInfoProvider,
        ICliAnalyticsCollect cliAnalyticsCollect,
        IOptions<TiknasCliOptions> options,
        IJsonSerializer jsonSerializer,
        IApiKeyService apiKeyService)
    {
        SourceCodeStore = sourceCodeStore;
        NugetPackageInfoProvider = nugetPackageInfoProvider;
        CliAnalyticsCollect = cliAnalyticsCollect;
        Options = options.Value;
        JsonSerializer = jsonSerializer;
        ApiKeyService = apiKeyService;

        Logger = NullLogger<NugetPackageProjectBuilder>.Instance;
    }

    public async Task<ProjectBuildResult> BuildAsync(ProjectBuildArgs args)
    {
        var packageInfo = await GetPackageInfoAsync(args);

        var templateFile = await SourceCodeStore.GetAsync(
            args.TemplateName,
            SourceCodeTypes.NugetPackage,
            args.Version,
            null,
            args.ExtraProperties.ContainsKey(GetSourceCommand.Options.Preview.Long)
        );

        var apiKeyResult = await ApiKeyService.GetApiKeyOrNullAsync();
        if (apiKeyResult?.ApiKey != null)
        {
            args.ExtraProperties["api-key"] = apiKeyResult.ApiKey;
        }

        if (apiKeyResult?.LicenseCode != null)
        {
            args.ExtraProperties["license-code"] = apiKeyResult.LicenseCode;
        }

        var context = new ProjectBuildContext(
            null,
            null,
            packageInfo,
            null,
            templateFile,
            args
        );

        NugetPackageProjectBuildPipelineBuilder.Build(context).Execute();

        // Exclude unwanted or known options.
        var options = args.ExtraProperties
            .Where(x => !x.Key.Equals(CliConsts.Command, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => !x.Key.Equals(NewCommand.Options.OutputFolder.Long, StringComparison.InvariantCultureIgnoreCase) &&
                        !x.Key.Equals(NewCommand.Options.OutputFolder.Short, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => !x.Key.Equals(NewCommand.Options.Version.Long, StringComparison.InvariantCultureIgnoreCase) &&
                        !x.Key.Equals(NewCommand.Options.Version.Short, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => !x.Key.Equals(NewCommand.Options.TemplateSource.Short, StringComparison.InvariantCultureIgnoreCase) &&
                        !x.Key.Equals(NewCommand.Options.TemplateSource.Long, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Key).ToList();

        await CliAnalyticsCollect.CollectAsync(new CliAnalyticsCollectInputDto
        {
            Tool = Options.ToolName,
            Command = args.ExtraProperties.ContainsKey(CliConsts.Command) ? args.ExtraProperties[CliConsts.Command] : "",
            DatabaseProvider = null,
            IsTiered = null,
            UiFramework = null,
            Options = JsonSerializer.Serialize(options),
            ProjectName = null,
            TemplateName = args.TemplateName,
            TemplateVersion = templateFile.Version
        });

        return new ProjectBuildResult(context.Result.ZipContent, args.TemplateName);
    }

    private async Task<NugetPackageInfo> GetPackageInfoAsync(ProjectBuildArgs args)
    {
        return await NugetPackageInfoProvider.GetAsync(args.TemplateName);
    }
}
