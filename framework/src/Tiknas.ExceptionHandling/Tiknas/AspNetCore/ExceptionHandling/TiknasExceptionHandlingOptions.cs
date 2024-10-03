using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.ExceptionHandling;

public class TiknasExceptionHandlingOptions
{
    public bool SendExceptionsDetailsToClients { get; set; }

    public bool SendStackTraceToClients { get; set; }

    public List<Type> SendExceptionDataToClientTypes { get; set; }

    public TiknasExceptionHandlingOptions()
    {
        SendExceptionsDetailsToClients = false;
        SendStackTraceToClients = true;
        SendExceptionDataToClientTypes =
        [
            typeof(IBusinessException)
        ];
    }
}
