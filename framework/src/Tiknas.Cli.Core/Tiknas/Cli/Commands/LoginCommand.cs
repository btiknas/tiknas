using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Tiknas.Cli.Args;
using Tiknas.Cli.Auth;
using Tiknas.Cli.ProjectBuilding;
using Tiknas.Cli.Utils;
using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.Cli.Commands;

public class LoginCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "login";

    public ILogger<LoginCommand> Logger { get; set; }

    protected AuthService AuthService { get; }
    public ICancellationTokenProvider CancellationTokenProvider { get; }
    public IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }

    public LoginCommand(AuthService authService,
        ICancellationTokenProvider cancellationTokenProvider,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler)
    {
        AuthService = authService;
        CancellationTokenProvider = cancellationTokenProvider;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        Logger = NullLogger<LoginCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        if (!commandLineArgs.Options.ContainsKey("device"))
        {
            if (commandLineArgs.Target.IsNullOrEmpty())
            {
                throw new CliUsageException(
                    "Username name is missing!" +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var organization = commandLineArgs.Options.GetOrNull(Options.Organization.Short, Options.Organization.Long);

            if (await HasMultipleOrganizationAndThisNotSpecified(commandLineArgs, organization))
            {
                return;
            }

            var password = commandLineArgs.Options.GetOrNull(Options.Password.Short, Options.Password.Long);
            if (password == null)
            {
                Console.Write("Password: ");
                password = ConsoleHelper.ReadSecret();
                if (password.IsNullOrWhiteSpace())
                {
                    throw new CliUsageException(
                        "Password is missing!" +
                        Environment.NewLine + Environment.NewLine +
                        GetUsageInfo()
                    );
                }
            }

            try
            {
                await AuthService.LoginAsync(
                    commandLineArgs.Target,
                    password,
                    organization
                );
            }
            catch (Exception ex)
            {
                LogCliError(ex, commandLineArgs);
                return;
            }

            Logger.LogInformation($"Successfully logged in as '{commandLineArgs.Target}'");
        }
        else
        {
            try
            {
                await AuthService.DeviceLoginAsync();
            }
            catch (Exception ex)
            {
                LogCliError(ex, commandLineArgs);
                return;
            }

            var loginInfo = await AuthService.GetLoginInfoAsync();
            Logger.LogInformation($"Successfully logged in as '{loginInfo.Username}'");
        }
    }

    private async Task<bool> HasMultipleOrganizationAndThisNotSpecified(CommandLineArgs commandLineArgs, string organization)
    {
        if (string.IsNullOrWhiteSpace(organization) &&
            await AuthService.CheckMultipleOrganizationsAsync(commandLineArgs.Target))
        {
            Logger.LogError($"You have multiple organizations, please specify your organization with `--organization` parameter.");
            return true;
        }

        return false;
    }

    private void LogCliError(Exception ex, CommandLineArgs args)
    {
        if (ex.Message.Contains("Invalid username or password"))
        {
            Logger.LogError("Invalid username or password!");
            return;
        }

        if (ex.Message.Contains("RequiresTwoFactor"))
        {
            Logger.LogError("Two factor authentication is enabled for your account. Please use `tiknas login --device` command to login.");
            return;
        }

        if (TryGetErrorMessageFromHtmlPage(ex.Message, out var errorMsg))
        {
            Logger.LogError(errorMsg);
            return;
        }

        Logger.LogError(ex.Message);
    }

    private static bool TryGetErrorMessageFromHtmlPage(string htmlPage, out string errorMessage)
    {
        if (!htmlPage.Contains("error-page-container"))
        {
            errorMessage = null;
            return false;
        }

        var decodedHtml = HttpUtility.HtmlDecode(htmlPage);

        var error = Regex.Match(decodedHtml,
            @"<h2\ class=""text-danger.*"">(.*?)</h2>",
            RegexOptions.IgnoreCase |
            RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Singleline |
            RegexOptions.Multiline);

        if (error.Success && error.Groups.Count > 1)
        {
            errorMessage = error.Groups[1].Value;
            errorMessage = errorMessage
                .Replace("<eof/>", string.Empty)
                .Replace("</small>", string.Empty);

            return true;
        }

        errorMessage = null;
        return false;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  tiknas login <username>");
        sb.AppendLine("  tiknas login <username> -p <password>");
        sb.AppendLine("  tiknas login <username> --device");
        sb.AppendLine("");
        sb.AppendLine("Example:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas login john");
        sb.AppendLine("  tiknas login john -p 1234");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.de/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Sign in to " + CliUrls.AccountTiknasIo + ".";
    }

    public static class Options
    {
        public static class Organization
        {
            public const string Short = "o";
            public const string Long = "organization";
        }

        public static class Password
        {
            public const string Short = "p";
            public const string Long = "password";
        }
    }
}
