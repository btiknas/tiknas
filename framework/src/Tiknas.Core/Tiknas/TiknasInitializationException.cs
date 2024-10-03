using System;

namespace Tiknas;

public class TiknasInitializationException : TiknasException
{
    public TiknasInitializationException()
    {

    }

    public TiknasInitializationException(string message)
        : base(message)
    {

    }

    public TiknasInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
