using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Sms;

[Dependency(TryRegister = true)]
public class NullSmsSender : ISmsSender, ISingletonDependency
{
    public ILogger<NullSmsSender> Logger { get; set; }

    public NullSmsSender()
    {
        Logger = NullLogger<NullSmsSender>.Instance;
    }

    public Task SendAsync(SmsMessage smsMessage)
    {
        Logger.LogWarning($"SMS Sending was not implemented! Using {nameof(NullSmsSender)}:");

        Logger.LogWarning("Phone Number : " + smsMessage.PhoneNumber);
        Logger.LogWarning("SMS Text     : " + smsMessage.Text);

        return Task.CompletedTask;
    }
}
