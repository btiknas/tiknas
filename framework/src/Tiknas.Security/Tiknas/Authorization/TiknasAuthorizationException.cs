using System;
using Microsoft.Extensions.Logging;
using Tiknas.ExceptionHandling;
using Tiknas.Logging;

namespace Tiknas.Authorization;

/// <summary>
/// This exception is thrown on an unauthorized request.
/// </summary>
public class TiknasAuthorizationException : TiknasException, IHasLogLevel, IHasErrorCode
{
    /// <summary>
    /// Severity of the exception.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Creates a new <see cref="TiknasAuthorizationException"/> object.
    /// </summary>
    public TiknasAuthorizationException()
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="TiknasAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public TiknasAuthorizationException(string message)
        : base(message)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="TiknasAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public TiknasAuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="TiknasAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="code">Exception code</param>
    /// <param name="innerException">Inner exception</param>
    public TiknasAuthorizationException(string? message = null, string? code = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        LogLevel = LogLevel.Warning;
    }

    public TiknasAuthorizationException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}
