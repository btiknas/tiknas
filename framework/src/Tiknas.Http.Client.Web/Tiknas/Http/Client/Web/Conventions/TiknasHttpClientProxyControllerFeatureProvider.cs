using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Tiknas.Http.Client.Web.Conventions;

public class TiknasHttpClientProxyControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        return TiknasHttpClientProxyHelper.IsClientProxyService(typeInfo);
    }
}
