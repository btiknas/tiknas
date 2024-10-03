using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Sms.Aliyun;

[DependsOn(typeof(TiknasSmsAliyunModule))]
public class TiknasSmsAliyunTestsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasAliyunSmsOptions>(
            configuration.GetSection("TiknasAliyunSms")
        );
    }
}
