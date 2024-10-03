using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Sms.Aliyun;

[DependsOn(typeof(TiknasSmsModule))]
public class TiknasSmsAliyunModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasAliyunSmsOptions>(configuration.GetSection("TiknasAliyunSms"));
    }
}
