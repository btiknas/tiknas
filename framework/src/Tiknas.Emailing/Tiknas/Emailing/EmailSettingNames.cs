namespace Tiknas.Emailing;

/// <summary>
/// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
/// </summary>
public static class EmailSettingNames
{
    /// <summary>
    /// Tiknas.Net.Mail.DefaultFromAddress
    /// </summary>
    public const string DefaultFromAddress = "Tiknas.Mailing.DefaultFromAddress";

    /// <summary>
    /// Tiknas.Net.Mail.DefaultFromDisplayName
    /// </summary>
    public const string DefaultFromDisplayName = "Tiknas.Mailing.DefaultFromDisplayName";

    /// <summary>
    /// SMTP related email settings.
    /// </summary>
    public static class Smtp
    {
        /// <summary>
        /// Tiknas.Net.Mail.Smtp.Host
        /// </summary>
        public const string Host = "Tiknas.Mailing.Smtp.Host";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.Port
        /// </summary>
        public const string Port = "Tiknas.Mailing.Smtp.Port";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.UserName
        /// </summary>
        public const string UserName = "Tiknas.Mailing.Smtp.UserName";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.Password
        /// </summary>
        public const string Password = "Tiknas.Mailing.Smtp.Password";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.Domain
        /// </summary>
        public const string Domain = "Tiknas.Mailing.Smtp.Domain";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.EnableSsl
        /// </summary>
        public const string EnableSsl = "Tiknas.Mailing.Smtp.EnableSsl";

        /// <summary>
        /// Tiknas.Net.Mail.Smtp.UseDefaultCredentials
        /// </summary>
        public const string UseDefaultCredentials = "Tiknas.Mailing.Smtp.UseDefaultCredentials";
    }
}
