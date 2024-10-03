using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Data;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Security;
using Tiknas.Threading;
using Tiknas.Timing;

namespace Tiknas.Auditing;

[DependsOn(
    typeof(TiknasDataModule),
    typeof(TiknasJsonModule),
    typeof(TiknasTimingModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasThreadingModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasAuditingContractsModule)
    )]
public class TiknasAuditingModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(AuditingInterceptorRegistrar.RegisterIfNeeded);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var applicationName = context.Services.GetApplicationName();
        
        if (!applicationName.IsNullOrEmpty())
        {
            Configure<TiknasAuditingOptions>(options =>
            {
                options.ApplicationName = applicationName;
            });
        }
    }
}
