using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Tiknas.Sms.Aliyun;

public class AliyunSmsSender_Tests : TiknasSmsAliyunTestBase
{
    private readonly ISmsSender _smsSender;
    private readonly IConfiguration _configuration;

    public AliyunSmsSender_Tests()
    {
        _configuration = GetRequiredService<IConfiguration>();
        _smsSender = GetRequiredService<ISmsSender>();
    }

    [Fact]
    public async Task SendSms_Test()
    {
        var config = _configuration.GetSection("TiknasAliyunSms");

        // Please fill in the real parameters in the appsettings.json file.
        if (config["AccessKeyId"] == "<Enter your AccessKeyId from Aliyun>")
        {
            return;
        }

        var msg = new SmsMessage(config["TargetPhoneNumber"],
            config["TemplateParam"]);
        msg.Properties.Add("SignName", config["SignName"]);
        msg.Properties.Add("TemplateCode", config["TemplateCode"]);

        await _smsSender.SendAsync(msg);
    }
}
