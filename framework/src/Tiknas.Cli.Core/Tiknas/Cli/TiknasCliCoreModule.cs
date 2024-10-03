using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Cli.Commands;
using Tiknas.Cli.Commands.Internal;
using Tiknas.Cli.Http;
using Tiknas.Cli.ServiceProxying;
using Tiknas.Cli.ServiceProxying.Angular;
using Tiknas.Cli.ServiceProxying.CSharp;
using Tiknas.Cli.ServiceProxying.JavaScript;
using Tiknas.Domain;
using Tiknas.Http;
using Tiknas.IdentityModel;
using Tiknas.Json;
using Tiknas.Localization;
using Tiknas.Minify;
using Tiknas.Modularity;

namespace Tiknas.Cli;

[DependsOn(
    typeof(TiknasDddDomainModule),
    typeof(TiknasJsonModule),
    typeof(TiknasIdentityModelModule),
    typeof(TiknasMinifyModule),
    typeof(TiknasHttpModule),
    typeof(TiknasLocalizationModule)
)]
public class TiknasCliCoreModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient(CliConsts.HttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => new CliHttpClientHandler());

        context.Services.AddHttpClient(CliConsts.GithubHttpClientName, client =>
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MyAgent/1.0");
        });

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Configure<TiknasCliOptions>(options =>
        {
            options.Commands[HelpCommand.Name] = typeof(HelpCommand);
            options.Commands[PromptCommand.Name] = typeof(PromptCommand);
            options.Commands[NewCommand.Name] = typeof(NewCommand);
            options.Commands[GetSourceCommand.Name] = typeof(GetSourceCommand);
            options.Commands[UpdateCommand.Name] = typeof(UpdateCommand);
            options.Commands[AddPackageCommand.Name] = typeof(AddPackageCommand);
            options.Commands[AddModuleCommand.Name] = typeof(AddModuleCommand);
            options.Commands[ListModulesCommand.Name] = typeof(ListModulesCommand);
            options.Commands[ListTemplatesCommand.Name] = typeof(ListTemplatesCommand);
            options.Commands[LoginCommand.Name] = typeof(LoginCommand);
            options.Commands[LoginInfoCommand.Name] = typeof(LoginInfoCommand);
            options.Commands[LogoutCommand.Name] = typeof(LogoutCommand);
            options.Commands[GenerateProxyCommand.Name] = typeof(GenerateProxyCommand);
            options.Commands[RemoveProxyCommand.Name] = typeof(RemoveProxyCommand);
            options.Commands[SuiteCommand.Name] = typeof(SuiteCommand);
            options.Commands[SwitchToPreviewCommand.Name] = typeof(SwitchToPreviewCommand);
            options.Commands[SwitchToStableCommand.Name] = typeof(SwitchToStableCommand);
            options.Commands[SwitchToNightlyCommand.Name] = typeof(SwitchToNightlyCommand);
            options.Commands[SwitchToPreRcCommand.Name] = typeof(SwitchToPreRcCommand);
            options.Commands[SwitchToLocal.Name] = typeof(SwitchToLocal);
            options.Commands[TranslateCommand.Name] = typeof(TranslateCommand);
            options.Commands[BuildCommand.Name] = typeof(BuildCommand);
            options.Commands[BundleCommand.Name] = typeof(BundleCommand);
            options.Commands[CreateMigrationAndRunMigratorCommand.Name] = typeof(CreateMigrationAndRunMigratorCommand);
            options.Commands[InstallLibsCommand.Name] = typeof(InstallLibsCommand);
            options.Commands[CleanCommand.Name] = typeof(CleanCommand);
            options.Commands[CliCommand.Name] = typeof(CliCommand);
            options.Commands[ClearDownloadCacheCommand.Name] = typeof(ClearDownloadCacheCommand);
            options.Commands[RecreateInitialMigrationCommand.Name] = typeof(RecreateInitialMigrationCommand);

            options.DisabledModulesToAddToSolution.Add("Tiknas.LeptonXTheme.Pro");
            options.DisabledModulesToAddToSolution.Add("Tiknas.LeptonXTheme.Lite");
        });

        Configure<TiknasCliServiceProxyOptions>(options =>
        {
            options.Generators[JavaScriptServiceProxyGenerator.Name] = typeof(JavaScriptServiceProxyGenerator);
            options.Generators[AngularServiceProxyGenerator.Name] = typeof(AngularServiceProxyGenerator);
            options.Generators[CSharpServiceProxyGenerator.Name] = typeof(CSharpServiceProxyGenerator);
        });
    }
}
