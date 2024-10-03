using System;

namespace Tiknas;

/// <summary>
/// Base exception type for those are thrown by Tiknas system for Tiknas specific exceptions.
/// </summary>
public class TiknasException : Exception
{
    public TiknasException()
    {

    }

    public TiknasException(string? message)
        : base(message)
    {

    }

    public TiknasException(string? message, Exception? innerException)
        : base(message, innerException)
    {

    }
}
