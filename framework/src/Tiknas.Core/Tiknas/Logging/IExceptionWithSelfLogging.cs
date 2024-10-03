using Microsoft.Extensions.Logging;

namespace Tiknas.Logging;

public interface IExceptionWithSelfLogging
{
    void Log(ILogger logger);
}
