using System.Threading.Tasks;

namespace Tiknas.Sms;

public interface ISmsSender
{
    Task SendAsync(SmsMessage smsMessage);
}
