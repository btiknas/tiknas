using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas;

public interface ITiknasApplicationWithExternalServiceProvider : ITiknasApplication
{
    /// <summary>
    /// Sets the service provider, but not initializes the modules.
    /// </summary>
    void SetServiceProvider([NotNull] IServiceProvider serviceProvider);

    /// <summary>
    /// Sets the service provider and initializes all the modules.
    /// If <see cref="SetServiceProvider"/> was called before, the same
    /// <paramref name="serviceProvider"/> instance should be passed to this method.
    /// </summary>
    Task InitializeAsync([NotNull] IServiceProvider serviceProvider);

    /// <summary>
    /// Sets the service provider and initializes all the modules.
    /// If <see cref="SetServiceProvider"/> was called before, the same
    /// <paramref name="serviceProvider"/> instance should be passed to this method.
    /// </summary>
    void Initialize([NotNull] IServiceProvider serviceProvider);
}
