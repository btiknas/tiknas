using Tiknas.Http.Modeling;

namespace Tiknas.Http.ProxyScripting.Generators;

public interface IProxyScriptGenerator
{
    string CreateScript(ApplicationApiDescriptionModel model);
}
