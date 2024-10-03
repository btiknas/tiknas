using System.Text.Json.Nodes;
using Tiknas.DependencyInjection;
using Tiknas.SimpleStateChecking;

namespace Tiknas.Authorization.Permissions;

public class AuthenticatedSimpleStateCheckerSerializerContributor : 
    ISimpleStateCheckerSerializerContributor,
    ISingletonDependency
{
    public const string CheckerShortName = "A";
    
    public string? SerializeToJson<TState>(ISimpleStateChecker<TState> checker) 
        where TState : IHasSimpleStateCheckers<TState>
    {
        if (checker is not RequireAuthenticatedSimpleStateChecker<TState>)
        {
            return null;
        }

        var jsonObject = new JsonObject {
            ["T"] = CheckerShortName
        };

        return jsonObject.ToJsonString();
    }

    public ISimpleStateChecker<TState>? Deserialize<TState>(JsonObject jsonObject, TState state)
        where TState : IHasSimpleStateCheckers<TState>
    {
        if (jsonObject["T"]?.ToString() != CheckerShortName)
        {
            return null;
        }
        
        return new RequireAuthenticatedSimpleStateChecker<TState>();
    }
}