using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.ClientProxying;

public interface IClientProxyApiDescriptionFinder
{
    ActionApiDescriptionModel? FindAction(string methodName);

    ApplicationApiDescriptionModel GetApiDescription();
}
