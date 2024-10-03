using Shouldly;
using Xunit;

namespace Tiknas.ObjectMapping;

public class ContextSpecificMapper_Tests : TiknasObjectMappingTestBase
{
    [Fact]
    public void Should_Resolve_Correct_ObjectMappper_For_Specific_Context()
    {
        GetRequiredService<IObjectMapper<MappingContext1>>()
            .ShouldBeOfType(typeof(DefaultObjectMapper<MappingContext1>));

        GetRequiredService<IAutoObjectMappingProvider<MappingContext1>>()
            .ShouldBeOfType(typeof(Test1AutoObjectMappingProvider<MappingContext1>));

        GetRequiredService<IObjectMapper<MappingContext2>>()
            .ShouldBeOfType(typeof(DefaultObjectMapper<MappingContext2>));

        GetRequiredService<IAutoObjectMappingProvider<MappingContext2>>()
            .ShouldBeOfType(typeof(Test2AutoObjectMappingProvider<MappingContext2>));
    }
}
