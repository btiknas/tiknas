using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tiknas.DependencyInjection;

namespace Tiknas.UI.Navigation;

public class MenuConfigurationContext : IMenuConfigurationContext
{
    public IServiceProvider ServiceProvider { get; }

    private readonly ITiknasLazyServiceProvider _lazyServiceProvider;

    public IAuthorizationService AuthorizationService => _lazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

    public IStringLocalizerFactory StringLocalizerFactory => _lazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    public ApplicationMenu Menu { get; }

    public MenuConfigurationContext(ApplicationMenu menu, IServiceProvider serviceProvider)
    {
        Menu = menu;
        ServiceProvider = serviceProvider;
        _lazyServiceProvider = ServiceProvider.GetRequiredService<ITiknasLazyServiceProvider>();
    }

    public Task<bool> IsGrantedAsync(string policyName)
    {
        return AuthorizationService.IsGrantedAsync(policyName);
    }

    public IStringLocalizer? GetDefaultLocalizer()
    {
        return StringLocalizerFactory.CreateDefaultOrNull();
    }

    [NotNull]
    public IStringLocalizer GetLocalizer<T>()
    {
        return StringLocalizerFactory.Create<T>();
    }

    [NotNull]
    public IStringLocalizer GetLocalizer(Type resourceType)
    {
        return StringLocalizerFactory.Create(resourceType);
    }
}
