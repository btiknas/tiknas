using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Args;
using Tiknas.Cli.Auth;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class LogoutCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "logout";
    
    public ILogger<LogoutCommand> Logger { get; set; }

    protected AuthService AuthService { get; }

    public LogoutCommand(AuthService authService)
    {
        AuthService = authService;
        Logger = NullLogger<LogoutCommand>.Instance;
    }

    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        return AuthService.LogoutAsync();
    }

    public string GetUsageInfo()
    {
        return string.Empty;
    }

    public static string GetShortDescription()
    {
        return "Sign out from " + CliUrls.AccountTiknasIo + ".";
    }
}
