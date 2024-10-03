using System.Threading.Tasks;

namespace Tiknas.Emailing;

/// <summary>
/// Defines configurations used while sending emails.
/// </summary>
public interface IEmailSenderConfiguration
{
    Task<string> GetDefaultFromAddressAsync();

    Task<string> GetDefaultFromDisplayNameAsync();
}
