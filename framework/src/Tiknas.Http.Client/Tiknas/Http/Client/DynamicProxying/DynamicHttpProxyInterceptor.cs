using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.Client.Proxying;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.DynamicProxying;

public class DynamicHttpProxyInterceptor<TService> : TiknasInterceptor, ITransientDependency
{

    // ReSharper disable once StaticMemberInGenericType
    protected static MethodInfo CallRequestAsyncMethod { get; }

    static DynamicHttpProxyInterceptor()
    {
        CallRequestAsyncMethod = typeof(DynamicHttpProxyInterceptor<TService>)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .First(m => m.Name == nameof(CallRequestAsync) && m.IsGenericMethodDefinition);
    }

    public ILogger<DynamicHttpProxyInterceptor<TService>> Logger { get; set; }
    protected DynamicHttpProxyInterceptorClientProxy<TService> InterceptorClientProxy { get; }
    protected TiknasHttpClientOptions ClientOptions { get; }
    protected IProxyHttpClientFactory HttpClientFactory { get; }
    protected IRemoteServiceConfigurationProvider RemoteServiceConfigurationProvider { get; }
    protected IApiDescriptionFinder ApiDescriptionFinder { get; }

    public DynamicHttpProxyInterceptor(
        DynamicHttpProxyInterceptorClientProxy<TService> interceptorClientProxy,
        IOptions<TiknasHttpClientOptions> clientOptions,
        IProxyHttpClientFactory httpClientFactory,
        IRemoteServiceConfigurationProvider remoteServiceConfigurationProvider,
        IApiDescriptionFinder apiDescriptionFinder)
    {
        InterceptorClientProxy = interceptorClientProxy;
        HttpClientFactory = httpClientFactory;
        RemoteServiceConfigurationProvider = remoteServiceConfigurationProvider;
        ApiDescriptionFinder = apiDescriptionFinder;
        ClientOptions = clientOptions.Value;

        Logger = NullLogger<DynamicHttpProxyInterceptor<TService>>.Instance;
    }

    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        var context = new ClientProxyRequestContext(
            await GetActionApiDescriptionModel(invocation),
            invocation.ArgumentsDictionary,
            typeof(TService));

        if (invocation.Method.ReturnType.GenericTypeArguments.IsNullOrEmpty())
        {
            await InterceptorClientProxy.CallRequestAsync(context);
        }
        else
        {
            var returnType = invocation.Method.ReturnType.GenericTypeArguments[0];
            var result = (Task)CallRequestAsyncMethod
                .MakeGenericMethod(returnType)
                .Invoke(this, new object[] { context })!;

            invocation.ReturnValue = await GetResultAsync(result, returnType);
        }
    }

    protected virtual async Task<ActionApiDescriptionModel> GetActionApiDescriptionModel(ITiknasMethodInvocation invocation)
    {
        var clientConfig = ClientOptions.HttpClientProxies.GetOrDefault(typeof(TService)) ??
                           throw new TiknasException($"Could not get DynamicHttpClientProxyConfig for {typeof(TService).FullName}.");
        var remoteServiceConfig = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(clientConfig.RemoteServiceName);
        var client = HttpClientFactory.Create(clientConfig.RemoteServiceName);

        return await ApiDescriptionFinder.FindActionAsync(
            client,
            remoteServiceConfig.BaseUrl,
            typeof(TService),
            invocation.Method
        );
    }

    protected virtual async Task<T> CallRequestAsync<T>(ClientProxyRequestContext context)
    {
        return await InterceptorClientProxy.CallRequestAsync<T>(context);
    }

    protected virtual async Task<object> GetResultAsync(Task task, Type resultType)
    {
        await task;
        var resultProperty = typeof(Task<>)
            .MakeGenericType(resultType)
            .GetProperty(nameof(Task<object>.Result), BindingFlags.Instance | BindingFlags.Public);
        Check.NotNull(resultProperty, nameof(resultProperty));
        return resultProperty!.GetValue(task)!;
    }
}
