using System;

namespace Tiknas.BlobStoring;

public class BlobAlreadyExistsException : TiknasException
{
    public BlobAlreadyExistsException()
    {

    }

    public BlobAlreadyExistsException(string message)
        : base(message)
    {

    }

    public BlobAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
