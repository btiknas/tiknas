using JetBrains.Annotations;

namespace Tiknas;

public interface IApplicationInfoAccessor
{
    /// <summary>
    /// Name of the application.
    /// This is useful for systems with multiple applications, to distinguish
    /// resources of the applications located together.
    /// </summary>
    string? ApplicationName { get; }
    
    /// <summary>
    /// A unique identifier for this application instance.
    /// This value changes whenever the application is restarted.
    /// </summary>
    [NotNull]
    string InstanceId { get; }
}