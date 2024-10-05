using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using NuGet.Versioning;
using Tiknas.Cli.Args;
using Tiknas.Cli.Auth;
using Tiknas.Cli.Commands.Services;
using Tiknas.Cli.Http;
using Tiknas.Cli.Version;
using Tiknas.Cli.Utils;
using Tiknas.DependencyInjection;
using Tiknas.Http;
using Tiknas.Json;
using Tiknas.Threading;

namespace Tiknas.Cli.Commands;

public class SuiteCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "suite";

    public ICmdHelper CmdHelper { get; }
    private readonly TiknasNuGetIndexUrlService _nuGetIndexUrlService;
    private readonly PackageVersionCheckerService _packageVersionCheckerService;
    private readonly AuthService _authService;
    private readonly CliHttpClientFactory _cliHttpClientFactory;
    private readonly SuiteAppSettingsService _suiteAppSettingsService;
    private const string SuitePackageName = "Tiknas.Suite";
    public ILogger<SuiteCommand> Logger { get; set; }

    private int _tiknasSuitePort = 3000;

    public SuiteCommand(
        TiknasNuGetIndexUrlService nuGetIndexUrlService,
        PackageVersionCheckerService packageVersionCheckerService,
        ICmdHelper cmdHelper,
        AuthService authService,
        CliHttpClientFactory cliHttpClientFactory,
        SuiteAppSettingsService suiteAppSettingsService)
    {
        CmdHelper = cmdHelper;
        _nuGetIndexUrlService = nuGetIndexUrlService;
        _packageVersionCheckerService = packageVersionCheckerService;
        _authService = authService;
        _cliHttpClientFactory = cliHttpClientFactory;
        _suiteAppSettingsService = suiteAppSettingsService;
        Logger = NullLogger<SuiteCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
#if !DEBUG    
        var loginInfo = await _authService.GetLoginInfoAsync();

        if (string.IsNullOrEmpty(loginInfo?.Organization))
        {
            throw new CliUsageException("Please login with your account.");
        }
#endif
        
        var operationType = NamespaceHelper.NormalizeNamespace(commandLineArgs.Target);

        var preview = commandLineArgs.Options.ContainsKey(Options.Preview.Short) ||
                      commandLineArgs.Options.ContainsKey(Options.Preview.Long);

        var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);
        var currentSuiteVersionAsString = GetCurrentSuiteVersion();

        switch (operationType)
        {
            case "":
            case null:
                await InstallSuiteIfNotInstalledAsync(currentSuiteVersionAsString);
                _tiknasSuitePort = await _suiteAppSettingsService.GetSuitePortAsync(currentSuiteVersionAsString);
                RunSuite(commandLineArgs);
                break;

            case "generate":
                await InstallSuiteIfNotInstalledAsync(currentSuiteVersionAsString);
                _tiknasSuitePort = await _suiteAppSettingsService.GetSuitePortAsync(currentSuiteVersionAsString);
                var suiteProcess = StartSuite();
                System.Threading.Thread.Sleep(500); //wait for initialization of the app
                await GenerateCrudPageAsync(commandLineArgs);
                if (suiteProcess != null)
                {
                    KillSuite();
                }

                break;

            case "install":
                await InstallSuiteAsync(version, preview);
                break;

            case "update":
                await UpdateSuiteAsync(version, preview);
                break;

            case "remove":
                Logger.LogInformation("Removing TIKNAS Suite...");
                RemoveSuite();
                break;
        }
    }

    private async Task GenerateCrudPageAsync(CommandLineArgs args)
    {
        var entityFile = args.Options.GetOrNull(Options.Crud.Entity.Short, Options.Crud.Entity.Long);
        var solutionFile = args.Options.GetOrNull(Options.Crud.Solution.Short, Options.Crud.Solution.Long);

        if (entityFile.IsNullOrEmpty() || !entityFile.EndsWith(".json") || !File.Exists(entityFile) ||
            solutionFile.IsNullOrEmpty() || !solutionFile.EndsWith(".sln"))
        {
            throw new UserFriendlyException("Invalid Arguments!");
        }

        Logger.LogInformation("Generating CRUD Page...");

        var client = _cliHttpClientFactory.CreateClient(false);
        var solutionId = await GetSolutionIdAsync(client, solutionFile);

        if (!solutionId.HasValue)
        {
            return;
        }

        var IsSolutionBuiltResponse = await client.GetAsync(
            $"http://localhost:{_tiknasSuitePort}/api/tiknasSuite/solutions/{solutionId.ToString()}/is-built"
        );
        
        var IsSolutionBuilt = Convert.ToBoolean(await IsSolutionBuiltResponse.Content.ReadAsStringAsync());

        if (!IsSolutionBuilt)
        {
            Logger.LogInformation("Building the solution...");
            CmdHelper.RunCmd("dotnet build", Path.GetDirectoryName(solutionFile));
        }

        var entityContent = new StringContent(
            File.ReadAllText(entityFile),
            Encoding.UTF8,
            MimeTypes.Application.Json
        );

        var responseMessage = await client.PostAsync(
            $"http://localhost:{_tiknasSuitePort}/api/tiknasSuite/crudPageGenerator/{solutionId.ToString()}/save-and-generate-entity",
            entityContent
        );

        var response = await responseMessage.Content.ReadAsStringAsync();

        if (!response.IsNullOrWhiteSpace())
        {
            Logger.LogError(response);

            if (response.Contains("Commercial.SuiteTemplates.dll"))
            {
                Logger.LogInformation("The solution should be built before generating an entity! Use `dotnet build` to build your solution.");
            }
        }
        else
        {
            Logger.LogInformation("CRUD page generation successfully completed.");
        }
    }

    private async Task<Guid?> GetSolutionIdAsync(HttpClient client, string solutionPath)
    {
        var timeIntervals = new List<TimeSpan>();
        for (var i = 0; i < 10; i++)
        {
            timeIntervals.Add(TimeSpan.FromSeconds(5));
        }

        var responseMessage = await client.GetHttpResponseMessageWithRetryAsync(
            $"http://localhost:{_tiknasSuitePort}/api/tiknasSuite/solutions",
            _cliHttpClientFactory.GetCancellationToken(TimeSpan.FromMinutes(10)),
            Logger,
            timeIntervals.ToArray());

        var response = await responseMessage.Content.ReadAsStringAsync();
        JArray solutions;

        try
        {
            solutions = (JArray)(JObject.Parse(response)["solutions"]);
        }
        catch (Exception)
        {
            Logger.LogError(response);
            return await AddSolutionToSuiteAsync(client, solutionPath);
        }

        foreach (JObject solution in solutions)
        {
            if (solution["path"].ToString() == solutionPath)
            {
                return Guid.Parse(solution["id"].ToString());
            }
        }

        return await AddSolutionToSuiteAsync(client, solutionPath);
    }

    private async Task<Guid?> AddSolutionToSuiteAsync(HttpClient client, string solutionPath)
    {
        var entityContent = new StringContent(
            "{\"Path\": \"" + solutionPath.Replace("\\", "\\\\") + "\"}",
            Encoding.UTF8,
            MimeTypes.Application.Json
        );

        var responseMessage = await client.PostAsync(
            $"http://localhost:{_tiknasSuitePort}/api/tiknasSuite/addSolution",
            entityContent,
            _cliHttpClientFactory.GetCancellationToken(TimeSpan.FromMinutes(10))
        );

        var response = await responseMessage.Content.ReadAsStringAsync();

        try
        {
            return Guid.Parse(JObject.Parse(response)["id"].ToString());
        }
        catch (Exception)
        {
            Logger.LogError(response);
            return null;
        }
    }

    private async Task InstallSuiteIfNotInstalledAsync(string currentSuiteVersion)
    {
        if (string.IsNullOrEmpty(currentSuiteVersion))
        {
            await InstallSuiteAsync();
        }
    }

    private string GetCurrentSuiteVersion()
    {
        var dotnetToolList = CmdHelper.RunCmdAndGetOutput("dotnet tool list -g", out int exitCode);

        var suiteLine = dotnetToolList.Split(Environment.NewLine)
            .FirstOrDefault(l => l.ToLower().StartsWith("tiknas.suite "));

        if (string.IsNullOrEmpty(suiteLine))
        {
            return null;
        }

        return suiteLine.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
    }

    private async Task InstallSuiteAsync(string version = null, bool preview = false)
    {
        var infoText = "Installing TIKNAS Suite ";
        if (version != null)
        {
            infoText += "v" + version + "... ";
        }
        else if (preview)
        {
            infoText += "latest preview version...";
        }
        else
        {
            infoText += "latest version...";
        }

        Logger.LogInformation(infoText);

        var nugetIndexUrl = await _nuGetIndexUrlService.GetAsync();

        if (nugetIndexUrl == null)
        {
            return;
        }

        try
        {
            var versionOption = string.Empty;

            if (preview)
            {
                var latestPreviewVersion = await GetLatestPreviewVersion();
                if (latestPreviewVersion != null)
                {
                    versionOption = $" --version {latestPreviewVersion}";
                    Logger.LogInformation("Latest preview version is " + latestPreviewVersion);
                }
            }
            else if (version != null)
            {
                versionOption = $" --version {version}";
            }

            CmdHelper.RunCmd(
                $"dotnet tool install {SuitePackageName}{versionOption} --add-source {nugetIndexUrl} -g",
                out int exitCode
            );

            if (exitCode == 0)
            {
                Logger.LogInformation("TIKNAS Suite has been successfully installed.");
                Logger.LogInformation("You can run it with the CLI command \"tiknas suite\"");
            }
            else
            {
                ShowSuiteManualInstallCommand();
            }
        }
        catch (Exception e)
        {
            Logger.LogError("Couldn't install TIKNAS Suite." + e.Message);
            ShowSuiteManualInstallCommand();
        }
    }

    private void ShowSuiteManualInstallCommand()
    {
        Logger.LogInformation("You can also run the following command to install TIKNAS Suite.");
        Logger.LogInformation(
            "dotnet tool install -g Tiknas.Suite --add-source https://nuget.tiknas.de/<your-private-key>/v3/index.json");
    }

    private async Task UpdateSuiteAsync(string version = null, bool preview = false)
    {
        var infoText = "Updating TIKNAS Suite ";
        if (version != null)
        {
            infoText += "to the " + version + "... ";
        }
        else if (preview)
        {
            infoText += "to the latest preview version...";
        }
        else
        {
            infoText += "...";
        }

        Logger.LogInformation(infoText);

        var nugetIndexUrl = await _nuGetIndexUrlService.GetAsync();
        if (nugetIndexUrl == null)
        {
            Logger.LogError("Cannot find your NuGet service URL!");
            return;
        }

        try
        {
            var versionOption = string.Empty;

            if (preview)
            {
                var latestPreviewVersion = await GetLatestPreviewVersion();
                if (latestPreviewVersion != null)
                {
                    versionOption = $" --version {latestPreviewVersion}";
                    Logger.LogInformation("Latest preview version is " + latestPreviewVersion);
                }
            }
            else if (version != null)
            {
                versionOption = $" --version {version}";
            }

            CmdHelper.RunCmd(
                $"dotnet tool update {SuitePackageName}{versionOption} --add-source {nugetIndexUrl} -g",
                out int exitCode
            );

            if (exitCode != 0)
            {
                ShowSuiteManualUpdateCommand();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError("Couldn't update TIKNAS Suite." + ex.Message);
            ShowSuiteManualUpdateCommand();
        }
    }

    private async Task<string> GetLatestPreviewVersion()
    {
        var latestPreviewVersionInfo = await _packageVersionCheckerService
            .GetLatestVersionOrNullAsync(
                packageId: SuitePackageName,
                includeReleaseCandidates: true
            );

        return latestPreviewVersionInfo.Version.IsPrerelease ? latestPreviewVersionInfo.Version.ToString() : null;
    }

    private void ShowSuiteManualUpdateCommand()
    {
        Logger.LogError("You can also run the following command to update TIKNAS Suite.");
        Logger.LogError(
            "dotnet tool update -g Tiknas.Suite --add-source https://nuget.tiknas.de/<your-private-key>/v3/index.json");
    }

    private void RemoveSuite()
    {
        CmdHelper.RunCmd("dotnet tool uninstall " + SuitePackageName + " -g");
    }

    private void RunSuite(CommandLineArgs commandLineArgs)
    {
        try
        {
            if (!GlobalToolHelper.IsGlobalToolInstalled("tiknas-suite"))
            {
                Logger.LogWarning(
                    "TIKNAS Suite is not installed! To install it you can run the command: \"tiknas suite install\"");
                return;
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning("Couldn't check TIKNAS Suite installed status: " + ex.Message);
        }
        
        var targetSolution = GetTargetSolutionOrNull(commandLineArgs);
        var launchUrl = targetSolution == null?
            $"http://localhost:{_tiknasSuitePort}":
            $"http://localhost:{_tiknasSuitePort}/CrudPageGenerator/Create?targetSolution={targetSolution}";
        
        if (IsSuiteAlreadyRunning())
        {
            Logger.LogInformation("Opening suite...");
            CmdHelper.Open(launchUrl);
            return;
        }

        if (IsPortAlreadyInUse())
        {
            Logger.LogError($"Port \"{_tiknasSuitePort}\" is already in use.");
            return;
        }

        if (targetSolution == null)
        {
            var args = Environment.GetCommandLineArgs();
            var suiteArgs = args.Skip(2).JoinAsString(" ");
            var command = string.Concat("tiknas-suite ", suiteArgs);

            CmdHelper.RunCmd(command);
        }
        else
        {
            new Thread(OpenSuiteInBrowserWithLaunchUrl).Start();
            
            CmdHelper.RunCmd("tiknas-suite --no-browser");
            
            void OpenSuiteInBrowserWithLaunchUrl()
            {
                Thread.Sleep(2500); // needed for suite to be ready.
                CmdHelper.Open(launchUrl);
            }
        }
    }

    private object GetTargetSolutionOrNull(CommandLineArgs commandLineArgs)
    {
        return commandLineArgs.Options.GetOrNull(Options.Crud.Solution.Short, Options.Crud.Solution.Long)
            ?? Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sln", SearchOption.TopDirectoryOnly).FirstOrDefault();
    }

    private Process StartSuite()
    {
        try
        {
            if (!GlobalToolHelper.IsGlobalToolInstalled("tiknas-suite"))
            {
                Logger.LogWarning("TIKNAS Suite is not installed! To install it you can run the command: \"tiknas suite install\"");
                return null;
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning("Couldn't check TIKNAS Suite installed status: " + ex.Message);
        }

        if (IsSuiteAlreadyRunning())
        {
            return null;
        }

        if (IsPortAlreadyInUse())
        {
            Logger.LogError($"Port \"{_tiknasSuitePort}\" is already in use.");
            return null;
        }

        return CmdHelper.RunCmdAndGetProcess("tiknas-suite --no-browser");
    }

    private bool IsSuiteAlreadyRunning()
    {
        return GetProcessesRelatedWithSuite().Any();
    }

    private bool IsPortAlreadyInUse()
    {
        var ipGP = IPGlobalProperties.GetIPGlobalProperties();
        var endpoints = ipGP.GetActiveTcpListeners();
        return endpoints.Any(e => e.Port == _tiknasSuitePort);
    }

    private void KillSuite()
    {
        try
        {
            var suiteProcesses = GetProcessesRelatedWithSuite();

            foreach (var suiteProcess in suiteProcesses)
            {
                suiteProcess.Kill();
                Logger.LogInformation("Suite closed.");
            }
        }
        catch (Exception ex)
        {
            Logger.LogInformation("Cannot close Suite." + ex.Message);
        }
    }

    private IEnumerable<Process> GetProcessesRelatedWithSuite()
    {
        return (from p in Process.GetProcesses()
            where p.ProcessName.ToLower().Contains("tiknas-suite")
            select p);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas suite [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("");
        sb.AppendLine("<no argument>                          (run TIKNAS Suite)");
        sb.AppendLine("install                                (install TIKNAS Suite as a dotnet global tool)");
        sb.AppendLine("update                                 (update TIKNAS Suite to the latest)");
        sb.AppendLine("remove                                 (uninstall TIKNAS Suite)");
        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas suite");
        sb.AppendLine("  tiknas suite install");
        sb.AppendLine("  tiknas suite install --preview");
        sb.AppendLine("  tiknas suite install --version 4.2.2");
        sb.AppendLine("  tiknas suite update");
        sb.AppendLine("  tiknas suite update --preview");
        sb.AppendLine("  tiknas suite remove");
        sb.AppendLine("");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Install, update, remove or start TIKNAS Suite. See https://tiknas.de/suite.";
    }

    public static class Options
    {
        public static class Preview
        {
            public const string Long = "preview";
            public const string Short = "p";
        }

        public static class Version
        {
            public const string Long = "version";
            public const string Short = "v";
        }

        public static class Crud
        {
            public static class Solution
            {
                public const string Long = "solution";
                public const string Short = "s";
            }

            public static class Entity
            {
                public const string Long = "entity";
                public const string Short = "e";
            }
        }
    }
}
