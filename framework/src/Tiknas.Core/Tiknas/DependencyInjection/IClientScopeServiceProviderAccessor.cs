using System;

namespace Tiknas.DependencyInjection;

public interface IClientScopeServiceProviderAccessor
{
    IServiceProvider ServiceProvider { get; }
}
