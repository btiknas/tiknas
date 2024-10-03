using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Tiknas.AspNetCore.ExceptionHandling;

public interface IHttpExceptionStatusCodeFinder
{
    HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception);
}
