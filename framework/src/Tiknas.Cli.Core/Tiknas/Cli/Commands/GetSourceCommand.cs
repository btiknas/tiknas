using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Args;
using Tiknas.Cli.Commands.Services;
using Tiknas.Cli.ProjectBuilding;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class GetSourceCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "get-source";
    
    private readonly SourceCodeDownloadService _sourceCodeDownloadService;
    public ModuleProjectBuilder ModuleProjectBuilder { get; }

    public ILogger<NewCommand> Logger { get; set; }

    public GetSourceCommand(ModuleProjectBuilder moduleProjectBuilder, SourceCodeDownloadService sourceCodeDownloadService)
    {
        _sourceCodeDownloadService = sourceCodeDownloadService;
        ModuleProjectBuilder = moduleProjectBuilder;
        Logger = NullLogger<NewCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        if (commandLineArgs.Target == null)
        {
            throw new CliUsageException(
                "Module name is missing!" +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);

        var outputFolder = GetOutPutFolder(commandLineArgs);

        var gitHubTiknasLocalRepositoryPath = commandLineArgs.Options.GetOrNull(Options.GitHubTiknasLocalRepositoryPath.Long);
        if (gitHubTiknasLocalRepositoryPath != null)
        {
            Logger.LogInformation("GitHub Tiknas Local Repository Path: " + gitHubTiknasLocalRepositoryPath);
        }

        commandLineArgs.Options.Add(CliConsts.Command, commandLineArgs.Command);

        await _sourceCodeDownloadService.DownloadModuleAsync(
            commandLineArgs.Target, outputFolder, version, gitHubTiknasLocalRepositoryPath, commandLineArgs.Options);
    }

    private static string GetOutPutFolder(CommandLineArgs commandLineArgs)
    {
        var outputFolder = commandLineArgs.Options.GetOrNull(Options.OutputFolder.Short, Options.OutputFolder.Long);
        if (outputFolder != null)
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            outputFolder = Path.GetFullPath(outputFolder);
        }
        else
        {
            outputFolder = Directory.GetCurrentDirectory();
        }

        return outputFolder;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas get-source <module-name> [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("");
        sb.AppendLine("-o|--output-folder <output-folder>          (default: current folder)");
        sb.AppendLine("-v|--version <version>                      (default: latest version)");
        sb.AppendLine("--preview                                   (Use latest pre-release version if there is at least one pre-release after latest stable version)");
        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas get-source Tiknas.Blogging");
        sb.AppendLine("  tiknas get-source Tiknas.Blogging -o d:\\my-project");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.io/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Download the source code of the specified module.";
    }

    public static class Options
    {
        public static class OutputFolder
        {
            public const string Short = "o";
            public const string Long = "output-folder";
        }

        public static class GitHubTiknasLocalRepositoryPath
        {
            public const string Long = "tiknas-path";
        }

        public static class Version
        {
            public const string Short = "v";
            public const string Long = "version";
        }

        public static class Preview
        {
            public const string Long = "preview";
        }
    }
}
