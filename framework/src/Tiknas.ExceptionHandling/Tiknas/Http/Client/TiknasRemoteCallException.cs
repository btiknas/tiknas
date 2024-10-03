using System;
using Tiknas.ExceptionHandling;

namespace Tiknas.Http.Client;

public class TiknasRemoteCallException : TiknasException, IHasErrorCode, IHasErrorDetails, IHasHttpStatusCode
{
    public int HttpStatusCode { get; set; }

    public string? Code => Error?.Code;

    public string? Details => Error?.Details;

    public RemoteServiceErrorInfo? Error { get; set; }

    public TiknasRemoteCallException()
    {

    }

    public TiknasRemoteCallException(string message, Exception? innerException = null)
        : base(message, innerException)
    {

    }

    public TiknasRemoteCallException(RemoteServiceErrorInfo error, Exception? innerException = null)
        : base(error.Message, innerException)
    {
        Error = error;

        if (error.Data != null)
        {
            foreach (var dataKey in error.Data.Keys)
            {
                Data[dataKey] = error.Data[dataKey];
            }
        }
    }
}
