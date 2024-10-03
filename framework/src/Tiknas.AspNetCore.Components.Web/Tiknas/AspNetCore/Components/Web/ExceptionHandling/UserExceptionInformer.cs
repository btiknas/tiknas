using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.AspNetCore.Components.ExceptionHandling;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Components.Messages;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.DependencyInjection;
using Tiknas.Http;

namespace Tiknas.AspNetCore.Components.Web.ExceptionHandling;

[Dependency(ReplaceServices = true)]
public class UserExceptionInformer : IUserExceptionInformer, IScopedDependency
{
    public ILogger<UserExceptionInformer> Logger { get; set; }
    protected IUiMessageService MessageService { get; }
    protected IExceptionToErrorInfoConverter ExceptionToErrorInfoConverter { get; }

    protected TiknasExceptionHandlingOptions Options { get; }

    public UserExceptionInformer(
        IUiMessageService messageService,
        IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
        IOptions<TiknasExceptionHandlingOptions> options)
    {
        MessageService = messageService;
        ExceptionToErrorInfoConverter = exceptionToErrorInfoConverter;
        Options = options.Value;
        Logger = NullLogger<UserExceptionInformer>.Instance;
    }

    public void Inform(UserExceptionInformerContext context)
    {
        //TODO: Create sync versions of the MessageService APIs.

        var errorInfo = GetErrorInfo(context);

        if (errorInfo.Details.IsNullOrEmpty())
        {
            MessageService.Error(errorInfo.Message!);
        }
        else
        {
            MessageService.Error(errorInfo.Details!, errorInfo.Message);
        }
    }

    public async Task InformAsync(UserExceptionInformerContext context)
    {
        var errorInfo = GetErrorInfo(context);

        if (errorInfo.Details.IsNullOrEmpty())
        {
            await MessageService.Error(errorInfo.Message!);
        }
        else
        {
            await MessageService.Error(errorInfo.Details!, errorInfo.Message);
        }
    }

    protected virtual RemoteServiceErrorInfo GetErrorInfo(UserExceptionInformerContext context)
    {
        return ExceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = Options.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = Options.SendStackTraceToClients;
            options.SendExceptionDataToClientTypes = Options.SendExceptionDataToClientTypes;
        });
    }
}
