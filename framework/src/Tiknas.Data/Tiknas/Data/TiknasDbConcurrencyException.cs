using System;

namespace Tiknas.Data;

public class TiknasDbConcurrencyException : TiknasException
{
    /// <summary>
    /// Creates a new <see cref="TiknasDbConcurrencyException"/> object.
    /// </summary>
    public TiknasDbConcurrencyException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="TiknasDbConcurrencyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public TiknasDbConcurrencyException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="TiknasDbConcurrencyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public TiknasDbConcurrencyException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
