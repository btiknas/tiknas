﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Args;
using Tiknas.Cli.ProjectModification;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class UpdateCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "update";
    
    public ILogger<UpdateCommand> Logger { get; set; }

    private readonly TiknasNugetPackagesVersionUpdater _nugetPackagesVersionUpdater;
    private readonly NpmPackagesUpdater _npmPackagesUpdater;

    public UpdateCommand(TiknasNugetPackagesVersionUpdater nugetPackagesVersionUpdater,
        NpmPackagesUpdater npmPackagesUpdater)
    {
        _nugetPackagesVersionUpdater = nugetPackagesVersionUpdater;
        _npmPackagesUpdater = npmPackagesUpdater;

        Logger = NullLogger<UpdateCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var updateNpm = commandLineArgs.Options.ContainsKey(Options.Packages.Npm);
        var updateNuget = commandLineArgs.Options.ContainsKey(Options.Packages.NuGet);

        var directory = commandLineArgs.Options.GetOrNull(Options.SolutionPath.Short, Options.SolutionPath.Long) ??
                        Directory.GetCurrentDirectory();
        var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);

        if (updateNuget || !updateNpm)
        {
            await UpdateNugetPackages(commandLineArgs, directory, version);
        }

        if (updateNpm || !updateNuget)
        {
            await UpdateNpmPackages(directory, version);
        }
    }

    private async Task UpdateNpmPackages(string directory, string version)
    {
        await _npmPackagesUpdater.Update(directory, version: version);
    }

    private async Task UpdateNugetPackages(CommandLineArgs commandLineArgs, string directory, string version)
    {
        var solutions = new List<string>();
        var givenSolution = commandLineArgs.Options.GetOrNull(Options.SolutionName.Short, Options.SolutionName.Long);
        
        if (givenSolution.IsNullOrWhiteSpace())
        {
            solutions.AddRange(Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories));
        }
        else
        {
            solutions.Add(givenSolution);
        }

        var checkAll = commandLineArgs.Options.ContainsKey(Options.CheckAll.Long);

        if (solutions.Any())
        {
            foreach (var solution in solutions)
            {
                var solutionName = Path.GetFileName(solution).RemovePostFix(".sln");

                await _nugetPackagesVersionUpdater.UpdateSolutionAsync(solution, checkAll: checkAll, version: version);

                Logger.LogInformation("Tiknas packages are updated in {SolutionName} solution", solutionName);
            }
            return;
        }

        var project = Directory.GetFiles(directory, "*.csproj").FirstOrDefault();

        if (project != null)
        {
            var projectName = Path.GetFileName(project).RemovePostFix(".csproj");

            await _nugetPackagesVersionUpdater.UpdateProjectAsync(project, checkAll: checkAll, version: version);

            Logger.LogInformation("Tiknas packages are updated in {ProjectName} project", projectName);
            return;
        }

        throw new CliUsageException(
            "No solution or project found in this directory." +
            Environment.NewLine + Environment.NewLine +
            GetUsageInfo()
        );
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas update [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-p|--include-previews                       (if supported by the template)");
        sb.AppendLine("--npm                                       (Only updates NPM packages)");
        sb.AppendLine("--nuget                                     (Only updates Nuget packages)");
        sb.AppendLine("-sp|--solution-path                         (Specify the solution path)");
        sb.AppendLine("-sn|--solution-name                         (Specify the solution name)");
        sb.AppendLine("--check-all                                 (Check the new version of each package separately)");
        sb.AppendLine("-v|--version <version>                      (default: latest version)");
        sb.AppendLine("");
        sb.AppendLine("Some examples:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas update");
        sb.AppendLine("  tiknas update -p");
        sb.AppendLine("  tiknas update -sp \"D:\\projects\\\" -sn Acme.BookStore");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.io/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Update all TIKNAS related NuGet packages and NPM packages in a solution or project to the latest version.";
    }

    public static class Options
    {
        public static class SolutionPath
        {
            public const string Short = "sp";
            public const string Long = "solution-path";
        }

        public static class SolutionName
        {
            public const string Short = "sn";
            public const string Long = "solution-name";
        }

        public static class Packages
        {
            public const string Npm = "npm";
            public const string NuGet = "nuget";
        }

        public static class CheckAll
        {
            public const string Long = "check-all";
        }

        public static class Version
        {
            public const string Short = "v";
            public const string Long = "version";
        }
    }
}
