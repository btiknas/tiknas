using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Authentication.OpenIdConnect;
using Tiknas.AspNetCore.MultiTenancy;
using Tiknas.Localization;
using Tiknas.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasOpenIdConnectExtensions
{
    public static AuthenticationBuilder AddTiknasOpenIdConnect(this AuthenticationBuilder builder)
        => builder.AddTiknasOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, _ => { });

    public static AuthenticationBuilder AddTiknasOpenIdConnect(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
        => builder.AddTiknasOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddTiknasOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
        => builder.AddTiknasOpenIdConnect(authenticationScheme, OpenIdConnectDefaults.DisplayName, configureOptions);

    public static AuthenticationBuilder AddTiknasOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdConnectOptions> configureOptions)
    {
        builder.Services.Configure<TiknasClaimsPrincipalFactoryOptions>(options =>
        {
            var openIdConnectOptions = new OpenIdConnectOptions();
            configureOptions?.Invoke(openIdConnectOptions);
            if (!openIdConnectOptions.Authority.IsNullOrEmpty())
            {
                options.RemoteRefreshUrl = openIdConnectOptions.Authority.RemovePostFix("/") + options.RemoteRefreshUrl;
            }
        });

        return builder.AddOpenIdConnect(authenticationScheme, displayName, options =>
        {
            options.ClaimActions.MapTiknasClaimTypes();

            options.Events ??= new OpenIdConnectEvents();
            var authorizationCodeReceived = options.Events.OnAuthorizationCodeReceived ?? (_ => Task.CompletedTask);

            options.Events.OnAuthorizationCodeReceived = receivedContext =>
            {
                SetTiknasTenantId(receivedContext);
                return authorizationCodeReceived.Invoke(receivedContext);
            };

            options.AccessDeniedPath = "/";

            options.Events.OnTokenValidated = async (context) =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TiknasAspNetCoreAuthenticationOpenIdConnectModule>>();
                var client = context.HttpContext.RequestServices.GetRequiredService<IOpenIdLocalUserCreationClient>();
                try
                {
                    await client.CreateOrUpdateAsync(context);
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                }

                var culture = context.ProtocolMessage.GetParameter("culture");
                var uiCulture = context.ProtocolMessage.GetParameter("ui-culture");
                if (CultureHelper.IsValidCultureCode(culture) && CultureHelper.IsValidCultureCode(uiCulture))
                {
                    context.Response.OnStarting(() =>
                    {
                        logger.LogInformation($"Setting culture and ui-culture to the response. culture: {culture}, ui-culture: {uiCulture}");

                        TiknasRequestCultureCookieHelper.SetCultureCookie(
                            context.HttpContext,
                            new RequestCulture(culture, uiCulture)
                        );

                        return Task.CompletedTask;
                    });
                }
                else
                {
                    logger.LogWarning($"Invalid culture or ui-culture parameter in the OpenIdConnect response. culture: {culture}, ui-culture: {uiCulture}");
                }
            };

            configureOptions?.Invoke(options);
        });
    }

    private static void SetTiknasTenantId(AuthorizationCodeReceivedContext receivedContext)
    {
        var tenantKey = receivedContext.HttpContext.RequestServices
            .GetRequiredService<IOptions<TiknasAspNetCoreMultiTenancyOptions>>().Value.TenantKey;

        if (receivedContext.Request.Cookies.ContainsKey(tenantKey))
        {
            receivedContext.TokenEndpointRequest?.SetParameter(tenantKey, receivedContext.Request.Cookies[tenantKey]);
        }
    }
}
