using System;

namespace Tiknas;

public class TiknasShutdownException : TiknasException
{
    public TiknasShutdownException()
    {

    }

    public TiknasShutdownException(string message)
        : base(message)
    {

    }

    public TiknasShutdownException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
