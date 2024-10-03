using System.Collections.Generic;
using Tiknas.Application.Services;
using Tiknas.Aspects;
using Tiknas.Auditing;
using Tiknas.Authorization;
using Tiknas.Domain;
using Tiknas.Features;
using Tiknas.GlobalFeatures;
using Tiknas.Http;
using Tiknas.Http.Modeling;
using Tiknas.Modularity;
using Tiknas.ObjectMapping;
using Tiknas.Security;
using Tiknas.Settings;
using Tiknas.Uow;
using Tiknas.Validation;

namespace Tiknas.Application;

[DependsOn(
    typeof(TiknasDddDomainModule),
    typeof(TiknasDddApplicationContractsModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasObjectMappingModule),
    typeof(TiknasValidationModule),
    typeof(TiknasAuthorizationModule),
    typeof(TiknasHttpAbstractionsModule),
    typeof(TiknasSettingsModule),
    typeof(TiknasFeaturesModule),
    typeof(TiknasGlobalFeaturesModule)
    )]
public class TiknasDddApplicationModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasApiDescriptionModelOptions>(options =>
        {
            options.IgnoredInterfaces.AddIfNotContains(typeof(IRemoteService));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IApplicationService));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IAuditingEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IValidationEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IGlobalFeatureCheckingEnabled));
        });
    }
}
