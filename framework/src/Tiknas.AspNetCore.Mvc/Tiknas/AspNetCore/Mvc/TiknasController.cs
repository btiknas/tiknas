﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Aspects;
using Tiknas.AspNetCore.Mvc.Validation;
using Tiknas.DependencyInjection;
using Tiknas.Features;
using Tiknas.Guids;
using Tiknas.Localization;
using Tiknas.MultiTenancy;
using Tiknas.ObjectMapping;
using Tiknas.Timing;
using Tiknas.UI.Navigation.Urls;
using Tiknas.Uow;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Mvc;

public abstract class TiknasController : Controller, IAvoidDuplicateCrossCuttingConcerns
{
    public ITiknasLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    [Obsolete("Use LazyServiceProvider instead.")]
    public IServiceProvider ServiceProvider { get; set; } = default!;

    protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

    protected Type? ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

    protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

    protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();

    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

    protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

    protected IUnitOfWork? CurrentUnitOfWork => UnitOfWorkManager?.Current;

    protected IClock Clock => LazyServiceProvider.LazyGetRequiredService<IClock>();

    protected IModelStateValidator ModelValidator => LazyServiceProvider.LazyGetRequiredService<IModelStateValidator>();

    protected IFeatureChecker FeatureChecker => LazyServiceProvider.LazyGetRequiredService<IFeatureChecker>();

    protected IAppUrlProvider AppUrlProvider => LazyServiceProvider.LazyGetRequiredService<IAppUrlProvider>();

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

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

    public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

    protected virtual void ValidateModel()
    {
        ModelValidator?.Validate(ModelState);
    }

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

    protected virtual async Task<RedirectResult> RedirectSafelyAsync(string returnUrl, string? returnUrlHash = null)
    {
        return Redirect(await GetRedirectUrlAsync(returnUrl, returnUrlHash));
    }

    protected virtual async Task<string> GetRedirectUrlAsync(string returnUrl, string? returnUrlHash = null)
    {
        returnUrl = await NormalizeReturnUrlAsync(returnUrl);

        if (!returnUrlHash.IsNullOrWhiteSpace())
        {
            returnUrl = returnUrl + returnUrlHash;
        }

        return returnUrl;
    }

    protected virtual async Task<string> NormalizeReturnUrlAsync(string returnUrl)
    {
        if (returnUrl.IsNullOrEmpty())
        {
            return await GetAppHomeUrlAsync();
        }

        if (Url.IsLocalUrl(returnUrl) || await AppUrlProvider.IsRedirectAllowedUrlAsync(returnUrl))
        {
            return returnUrl;
        }

        return await GetAppHomeUrlAsync();
    }

    protected virtual Task<string> GetAppHomeUrlAsync()
    {
        return Task.FromResult(Url.Content("~/"));
    }
}
