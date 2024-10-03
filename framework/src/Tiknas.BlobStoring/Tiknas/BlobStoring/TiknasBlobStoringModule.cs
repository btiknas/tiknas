using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Threading;

namespace Tiknas.BlobStoring;

[DependsOn(
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasThreadingModule)
    )]
public class TiknasBlobStoringModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(
            typeof(IBlobContainer<>),
            typeof(BlobContainer<>)
        );

        context.Services.AddTransient(
            typeof(IBlobContainer),
            serviceProvider => serviceProvider
                .GetRequiredService<IBlobContainer<DefaultContainer>>()
        );
    }
}
