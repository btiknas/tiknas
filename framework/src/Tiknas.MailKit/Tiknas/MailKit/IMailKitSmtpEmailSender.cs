using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Tiknas.Emailing;

namespace Tiknas.MailKit;

public interface IMailKitSmtpEmailSender : IEmailSender
{
    Task<SmtpClient> BuildClientAsync();
}
