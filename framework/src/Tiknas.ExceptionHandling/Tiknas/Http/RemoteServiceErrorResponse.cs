﻿namespace Tiknas.Http;

public class RemoteServiceErrorResponse
{
    public RemoteServiceErrorInfo Error { get; set; }

    public RemoteServiceErrorResponse(RemoteServiceErrorInfo error)
    {
        Error = error;
    }
}
