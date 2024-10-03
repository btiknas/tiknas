using Tiknas.TestBase;

namespace Tiknas.Sms.Aliyun;

public class TiknasSmsAliyunTestBase : TiknasIntegratedTest<TiknasSmsAliyunTestsModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
