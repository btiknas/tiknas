using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Tiknas.DependencyInjection;

namespace Tiknas.UI.Navigation;

public interface IMenuConfigurationContext : IServiceProviderAccessor
{
    ApplicationMenu Menu { get; }

    IAuthorizationService AuthorizationService { get; }

    IStringLocalizerFactory StringLocalizerFactory { get; }
}
