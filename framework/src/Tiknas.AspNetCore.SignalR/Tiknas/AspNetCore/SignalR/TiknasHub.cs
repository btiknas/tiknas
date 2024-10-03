using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.DependencyInjection;
using Tiknas.Localization;
using Tiknas.MultiTenancy;
using Tiknas.Timing;
using Tiknas.Users;

namespace Tiknas.AspNetCore.SignalR;

public abstract class TiknasHub : Hub
{
    public ITiknasLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    [Obsolete("Use LazyServiceProvider instead.")]
    public IServiceProvider ServiceProvider { get; set; } = default!;

    protected ILoggerFactory? LoggerFactory => LazyServiceProvider.LazyGetService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

    protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetService<ICurrentUser>()!;

    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetService<ICurrentTenant>()!;

    protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetService<IAuthorizationService>()!;

    protected IClock Clock => LazyServiceProvider.LazyGetService<IClock>()!;

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetService<IStringLocalizerFactory>()!;

    protected IStringLocalizer L {
        get {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }
    private IStringLocalizer? _localizer;

    protected Type? LocalizationResource {
        get => _localizationResource;
        set {
            _localizationResource = value;
            _localizer = null;
        }
    }
    private Type? _localizationResource = typeof(DefaultResource);

    protected virtual IStringLocalizer CreateLocalizer()
    {
        if (LocalizationResource != null)
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }

        var localizer = StringLocalizerFactory.CreateDefaultOrNull();
        if (localizer == null)
        {
            throw new TiknasException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(TiknasLocalizationOptions)}.{nameof(TiknasLocalizationOptions.DefaultResourceType)}) to be able to use the {nameof(L)} object!");
        }

        return localizer;
    }
}

public abstract class TiknasHub<T> : Hub<T>
    where T : class
{
    public ITiknasLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    public IServiceProvider ServiceProvider { get; set; } = default!;

    protected ILoggerFactory? LoggerFactory => LazyServiceProvider.LazyGetService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

    protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetService<ICurrentUser>()!;

    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetService<ICurrentTenant>()!;

    protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetService<IAuthorizationService>()!;

    protected IClock Clock => LazyServiceProvider.LazyGetService<IClock>()!;

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetService<IStringLocalizerFactory>()!;

    protected IStringLocalizer L {
        get {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }
    private IStringLocalizer? _localizer;

    protected Type? LocalizationResource {
        get => _localizationResource;
        set {
            _localizationResource = value;
            _localizer = null;
        }
    }
    private Type? _localizationResource = typeof(DefaultResource);

    protected virtual IStringLocalizer CreateLocalizer()
    {
        if (LocalizationResource != null)
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }

        var localizer = StringLocalizerFactory.CreateDefaultOrNull();
        if (localizer == null)
        {
            throw new TiknasException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(TiknasLocalizationOptions)}.{nameof(TiknasLocalizationOptions.DefaultResourceType)}) to be able to use the {nameof(L)} object!");
        }

        return localizer;
    }
}
