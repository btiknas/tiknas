using System;
using System.Linq;
using Tiknas.Application.Services;
using Tiknas.Http.Client.ClientProxying;

namespace Tiknas.Http.Client.Web.Conventions;

public static class TiknasHttpClientProxyHelper
{
    public static bool IsClientProxyService(Type type)
    {
        return typeof(IApplicationService).IsAssignableFrom(type) &&
            type.GetBaseClasses().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ClientProxyBase<>));
    }
}
