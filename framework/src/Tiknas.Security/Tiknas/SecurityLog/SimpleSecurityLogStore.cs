using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.SecurityLog;

public class SimpleSecurityLogStore : ISecurityLogStore, ITransientDependency
{
    public ILogger<SimpleSecurityLogStore> Logger { get; set; }
    protected TiknasSecurityLogOptions SecurityLogOptions { get; }

    public SimpleSecurityLogStore(ILogger<SimpleSecurityLogStore> logger, IOptions<TiknasSecurityLogOptions> securityLogOptions)
    {
        Logger = logger;
        SecurityLogOptions = securityLogOptions.Value;
    }

    public Task SaveAsync(SecurityLogInfo securityLogInfo)
    {
        if (!SecurityLogOptions.IsEnabled)
        {
            return Task.CompletedTask;
        }

        Logger.LogInformation(securityLogInfo.ToString());
        return Task.CompletedTask;
    }
}
