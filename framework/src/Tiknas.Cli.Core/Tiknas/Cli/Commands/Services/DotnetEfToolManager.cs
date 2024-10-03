using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Utils;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands.Services;

public class DotnetEfToolManager : ISingletonDependency
{
    public ICmdHelper CmdHelper { get; }
    public ILogger<DotnetEfToolManager> Logger { get; set; }

    public DotnetEfToolManager(ICmdHelper cmdHelper)
    {
        CmdHelper = cmdHelper;
        
        Logger = NullLogger<DotnetEfToolManager>.Instance;
    }

    public Task BeSureInstalledAsync()
    {
        if (IsDotNetEfToolInstalled())
        {
            return Task.CompletedTask;
        }

        InstallDotnetEfTool();
        return Task.CompletedTask;
    }

    private bool IsDotNetEfToolInstalled()
    {
        var output = CmdHelper.RunCmdAndGetOutput("dotnet tool list -g");
        return output.Contains("dotnet-ef");
    }

    private void InstallDotnetEfTool()
    {
        Logger.LogInformation("Installing dotnet-ef tool...");
        CmdHelper.RunCmd("dotnet tool install --global dotnet-ef");
        Logger.LogInformation("dotnet-ef tool is installed.");
    }
}