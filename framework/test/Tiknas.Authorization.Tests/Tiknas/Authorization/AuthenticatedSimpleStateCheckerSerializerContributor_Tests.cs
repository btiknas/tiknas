using System.Collections.Generic;
using System.Text.Json.Nodes;
using Shouldly;
using Tiknas.Authorization.Permissions;
using Tiknas.SimpleStateChecking;
using Xunit;

namespace Tiknas.Authorization;

public class AuthenticatedSimpleStateCheckerSerializerContributor_Tests
{
    [Fact]
    public void Should_Serialize_RequireGlobalFeaturesSimpleStateChecker()
    {
        var serializer = new AuthenticatedSimpleStateCheckerSerializerContributor();
        
        var result = serializer.SerializeToJson(
            new RequireAuthenticatedSimpleStateChecker<MyState>()
        );
        
        result.ShouldBe("{\"T\":\"A\"}");
    }
    
    [Fact]
    public void Should_Deserialize_RequireGlobalFeaturesSimpleStateChecker()
    {
        var serializer = new AuthenticatedSimpleStateCheckerSerializerContributor();

        var jsonObject = (JsonObject)JsonNode.Parse("{\"T\":\"A\"}");
        var checker = serializer.Deserialize(jsonObject, new MyState());
        
        checker.ShouldBeOfType<RequireAuthenticatedSimpleStateChecker<MyState>>();
        var globalFeaturesSimpleStateChecker = checker as RequireAuthenticatedSimpleStateChecker<MyState>;
        globalFeaturesSimpleStateChecker.ShouldNotBeNull();
    }

    private class MyState : IHasSimpleStateCheckers<MyState>
    {
        public List<ISimpleStateChecker<MyState>> StateCheckers { get; } = new();
    }
}