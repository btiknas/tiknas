using System.Net.Mail;
using System.Threading.Tasks;
using Tiknas.BackgroundJobs;
using Tiknas.DependencyInjection;
using Tiknas.Emailing;
using Tiknas.Emailing.Smtp;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using Tiknas.MultiTenancy;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Tiknas.MailKit;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
public class MailKitSmtpEmailSender : EmailSenderBase, IMailKitSmtpEmailSender
{
    protected TiknasMailKitOptions TiknasMailKitOptions { get; }

    protected ISmtpEmailSenderConfiguration SmtpConfiguration { get; }

    public MailKitSmtpEmailSender(
        ICurrentTenant currentTenant,
        ISmtpEmailSenderConfiguration smtpConfiguration,
        IBackgroundJobManager backgroundJobManager,
        IOptions<TiknasMailKitOptions> tiknasMailKitConfiguration)
        : base(currentTenant, smtpConfiguration, backgroundJobManager)
    {
        TiknasMailKitOptions = tiknasMailKitConfiguration.Value;
        SmtpConfiguration = smtpConfiguration;
    }

    protected async override Task SendEmailAsync(MailMessage mail)
    {
        using (var client = await BuildClientAsync())
        {
            var message = MimeMessage.CreateFromMailMessage(mail);
            message.MessageId = MimeUtils.GenerateMessageId();
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task<SmtpClient> BuildClientAsync()
    {
        var client = new SmtpClient();

        try
        {
            await ConfigureClient(client);
            return client;
        }
        catch
        {
            client.Dispose();
            throw;
        }
    }

    protected virtual async Task ConfigureClient(SmtpClient client)
    {
        await client.ConnectAsync(
            await SmtpConfiguration.GetHostAsync(),
            await SmtpConfiguration.GetPortAsync(),
            await GetSecureSocketOption()
        );

        if (await SmtpConfiguration.GetUseDefaultCredentialsAsync())
        {
            return;
        }

        await client.AuthenticateAsync(
            await SmtpConfiguration.GetUserNameAsync(),
            await SmtpConfiguration.GetPasswordAsync()
        );
    }

    protected virtual async Task<SecureSocketOptions> GetSecureSocketOption()
    {
        if (TiknasMailKitOptions.SecureSocketOption.HasValue)
        {
            return TiknasMailKitOptions.SecureSocketOption.Value;
        }

        return await SmtpConfiguration.GetEnableSslAsync()
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.StartTlsWhenAvailable;
    }
}
