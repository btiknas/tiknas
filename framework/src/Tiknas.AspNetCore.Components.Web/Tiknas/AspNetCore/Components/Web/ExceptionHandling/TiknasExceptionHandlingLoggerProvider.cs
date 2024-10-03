using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tiknas.AspNetCore.Components.Web.ExceptionHandling;

public class TiknasExceptionHandlingLoggerProvider : ILoggerProvider
{
    private TiknasExceptionHandlingLogger? _logger;
    private static readonly object SyncObj = new object();
    private readonly IServiceCollection _serviceCollection;

    public TiknasExceptionHandlingLoggerProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_logger == null)
        {
            lock (SyncObj)
            {
                if (_logger == null)
                {
                    _logger = new TiknasExceptionHandlingLogger(_serviceCollection);
                }
            }
        }

        return _logger;
    }

    public void Dispose()
    {

    }
}
