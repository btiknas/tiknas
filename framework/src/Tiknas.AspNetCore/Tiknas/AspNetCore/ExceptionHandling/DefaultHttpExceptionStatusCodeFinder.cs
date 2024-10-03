using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tiknas.Authorization;
using Tiknas.Data;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Entities;
using Tiknas.ExceptionHandling;
using Tiknas.Validation;

namespace Tiknas.AspNetCore.ExceptionHandling;

public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder, ITransientDependency
{
    protected TiknasExceptionHttpStatusCodeOptions Options { get; }

    public DefaultHttpExceptionStatusCodeFinder(
        IOptions<TiknasExceptionHttpStatusCodeOptions> options)
    {
        Options = options.Value;
    }

    public virtual HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
    {
        if (exception is IHasHttpStatusCode exceptionWithHttpStatusCode &&
            exceptionWithHttpStatusCode.HttpStatusCode > 0)
        {
            return (HttpStatusCode)exceptionWithHttpStatusCode.HttpStatusCode;
        }

        if (exception is IHasErrorCode exceptionWithErrorCode &&
            !exceptionWithErrorCode.Code.IsNullOrWhiteSpace())
        {
            if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue(exceptionWithErrorCode.Code!, out var status))
            {
                return status;
            }
        }

        if (exception is TiknasAuthorizationException)
        {
            return httpContext.User.Identity!.IsAuthenticated
                ? HttpStatusCode.Forbidden
                : HttpStatusCode.Unauthorized;
        }

        //TODO: Handle SecurityException..?

        if (exception is TiknasValidationException)
        {
            return HttpStatusCode.BadRequest;
        }

        if (exception is EntityNotFoundException)
        {
            return HttpStatusCode.NotFound;
        }

        if (exception is TiknasDbConcurrencyException)
        {
            return HttpStatusCode.Conflict;
        }

        if (exception is NotImplementedException)
        {
            return HttpStatusCode.NotImplemented;
        }

        if (exception is IBusinessException)
        {
            return HttpStatusCode.Forbidden;
        }

        return HttpStatusCode.InternalServerError;
    }
}
