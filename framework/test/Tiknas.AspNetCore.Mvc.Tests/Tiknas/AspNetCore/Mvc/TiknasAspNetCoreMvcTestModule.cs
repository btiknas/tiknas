using System;
using System.Collections.Generic;
using System.Security.Claims;
using Localization.Resources.TiknasUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.AspNetCore.Mvc.GlobalFeatures;
using Tiknas.AspNetCore.Mvc.Libs;
using Tiknas.AspNetCore.Mvc.Localization;
using Tiknas.AspNetCore.Mvc.Localization.Resource;
using Tiknas.AspNetCore.Security.Claims;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Authorization;
using Tiknas.Autofac;
using Tiknas.GlobalFeatures;
using Tiknas.Localization;
using Tiknas.MemoryDb;
using Tiknas.Modularity;
using Tiknas.TestApp;
using Tiknas.TestApp.Application;
using Tiknas.Threading;
using Tiknas.Validation.Localization;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasMemoryDbTestModule),
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasAspNetCoreMvcTestModule : TiknasModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<TiknasMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MvcTestResource),
                typeof(TiknasAspNetCoreMvcTestModule).Assembly
            );
        });

        context.Services.PreConfigure<TiknasAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(TestAppModule).Assembly, opts =>
            {
                opts.UrlActionNameNormalizer = urlActionNameNormalizerContext =>
                    string.Equals(urlActionNameNormalizerContext.ActionNameInUrl, "phone", StringComparison.OrdinalIgnoreCase)
                        ? "phones"
                        : urlActionNameNormalizerContext.ActionNameInUrl;

                opts.TypePredicate = type => type != typeof(ConventionalAppService);
            });

            options.ExposeIntegrationServices = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.GetOrAdd(TiknasAspNetCoreMvcTestFeatures.ModuleName,
                () => new TiknasAspNetCoreMvcTestFeatures(GlobalFeatureManager.Instance))
                .EnableAll();
        });

        context.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = FakeAuthenticationSchemeDefaults.Scheme;
            options.DefaultChallengeScheme = "Bearer";
            options.DefaultForbidScheme = "Cookie";
        }).AddFakeAuthentication().AddCookie("Cookie").AddJwtBearer("Bearer", _ => { });

        context.Services.AddAuthorization(options =>
        {
            options.AddPolicy("MyClaimTestPolicy", policy =>
            {
                policy.RequireClaim("MyCustomClaimType", "42");
            });

            options.AddPolicy("TestPermission1_And_TestPermission2", policy =>
            {
                policy.Requirements.Add(new PermissionsRequirement(new []{"TestPermission1", "TestPermission2"}, requiresAll: true));
            });

            options.AddPolicy("TestPermission1_Or_TestPermission2", policy =>
            {
                policy.Requirements.Add(new PermissionsRequirement(new []{"TestPermission1", "TestPermission2"}, requiresAll: false));
            });
        });

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcTestModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MvcTestResource>("en")
                .AddBaseTypes(
                    typeof(TiknasUiResource),
                    typeof(TiknasValidationResource)
                ).AddVirtualJson("/Tiknas/AspNetCore/Mvc/Localization/Resource");

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.RootDirectory = "/Tiknas/AspNetCore/Mvc";
        });

        Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationFormats.Add("/Tiknas/AspNetCore/App/Views/{1}/{0}.cshtml");
        });

        Configure<TiknasClaimsMapOptions>(options =>
        {
            options.Maps.Add("SerialNumber", () => ClaimTypes.SerialNumber);
            options.Maps.Add("DateOfBirth", () => ClaimTypes.DateOfBirth);
        });

        Configure<TiknasApplicationConfigurationOptions>(options =>
        {
            options.Contributors.Add(new TestApplicationConfigurationContributor());
        });

        context.Services.TransformTiknasClaims();

        Configure<TiknasMvcLibsOptions>(options =>
        {
            options.CheckLibs = false;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseTiknasRequestLocalization();
        app.UseTiknasSecurityHeaders();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}
