using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Cli.ServiceProxying;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class GenerateProxyCommand : ProxyCommandBase<GenerateProxyCommand>
{
    public const string Name = "generate-proxy";

    protected override string CommandName => Name;

    public GenerateProxyCommand(
        IOptions<TiknasCliServiceProxyOptions> serviceProxyOptions,
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceProxyOptions, serviceScopeFactory)
    {
    }

    public override string GetUsageInfo()
    {
        var sb = new StringBuilder(base.GetUsageInfo());

        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas generate-proxy -t ng");
        sb.AppendLine("  tiknas generate-proxy -t js -m identity -o Pages/Identity/client-proxies.js -url https://localhost:44302/");
        sb.AppendLine("  tiknas generate-proxy -t csharp --folder MyProxies/InnerFolder -url https://localhost:44302/");
        sb.AppendLine("  tiknas generate-proxy -t csharp -url https://localhost:44302/ --without-contracts");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Generates client service proxies and DTOs to consume HTTP APIs.";
    }
}
