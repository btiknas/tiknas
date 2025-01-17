﻿using System.Threading.Tasks;

namespace Tiknas.Emailing.Smtp;

/// <summary>
/// Defines configurations to used by SmtpClient object.
/// </summary>
public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration
{
    /// <summary>
    /// SMTP Host name/IP.
    /// </summary>
    Task<string> GetHostAsync();

    /// <summary>
    /// SMTP Port.
    /// </summary>
    Task<int> GetPortAsync();

    /// <summary>
    /// User name to login to SMTP server.
    /// </summary>
    Task<string> GetUserNameAsync();

    /// <summary>
    /// Password to login to SMTP server.
    /// </summary>
    Task<string> GetPasswordAsync();

    /// <summary>
    /// Domain name to login to SMTP server.
    /// </summary>
    Task<string?> GetDomainAsync();

    /// <summary>
    /// Is SSL enabled?
    /// </summary>
    Task<bool> GetEnableSslAsync();

    /// <summary>
    /// Use default credentials?
    /// </summary>
    Task<bool> GetUseDefaultCredentialsAsync();
}
