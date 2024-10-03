using Shouldly;
using Tiknas.Data;
using Tiknas.Json.SystemTextJson;
using Tiknas.ObjectExtending;
using Xunit;

namespace Tiknas.Json;

public class ExtensibleObjectModifiers_Tests : TiknasJsonSystemTextJsonTestBase
{
    [Fact]
    public void Should_Modify_Object()
    {
        var jsonSerializer = GetRequiredService<TiknasSystemTextJsonSerializer>();

        var extensibleObject = jsonSerializer.Deserialize<ExtensibleObject>("{\"ExtraProperties\": {\"Test-Key\":\"Test-Value\"}}");
        extensibleObject.ExtraProperties.ShouldContainKeyAndValue("Test-Key", "Test-Value");

        var bar = jsonSerializer.Deserialize<Bar>("{\"ExtraProperties\": {\"Test-Key\":\"Test-Value\"}}");
        bar.ExtraProperties.ShouldContainKeyAndValue("Test-Key", "Test-Value");
    }
}

public abstract class Foo : IHasExtraProperties
{
    public ExtraPropertyDictionary ExtraProperties { get; protected set; }
}

public class Bar : Foo
{

}
