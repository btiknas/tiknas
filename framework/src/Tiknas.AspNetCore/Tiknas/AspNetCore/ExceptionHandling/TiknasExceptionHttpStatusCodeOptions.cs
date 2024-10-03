using System.Collections.Generic;
using System.Net;

namespace Tiknas.AspNetCore.ExceptionHandling;

public class TiknasExceptionHttpStatusCodeOptions
{
    public IDictionary<string, HttpStatusCode> ErrorCodeToHttpStatusCodeMappings { get; }

    public TiknasExceptionHttpStatusCodeOptions()
    {
        ErrorCodeToHttpStatusCodeMappings = new Dictionary<string, HttpStatusCode>();
    }

    public void Map(string errorCode, HttpStatusCode httpStatusCode)
    {
        ErrorCodeToHttpStatusCodeMappings[errorCode] = httpStatusCode;
    }
}
