using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas;

public static class ServiceProviderAccessorExtensions
{
    public static HttpContext? GetHttpContext(this IServiceProviderAccessor serviceProviderAccessor)
    {
        return serviceProviderAccessor.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    }
}
