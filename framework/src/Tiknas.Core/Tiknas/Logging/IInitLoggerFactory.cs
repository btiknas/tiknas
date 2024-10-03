namespace Tiknas.Logging;

public interface IInitLoggerFactory
{
    IInitLogger<T> Create<T>();
}
