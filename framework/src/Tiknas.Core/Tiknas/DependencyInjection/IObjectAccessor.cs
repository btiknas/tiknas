using JetBrains.Annotations;

namespace Tiknas.DependencyInjection;

public interface IObjectAccessor<out T>
{
    T? Value { get; }
}
