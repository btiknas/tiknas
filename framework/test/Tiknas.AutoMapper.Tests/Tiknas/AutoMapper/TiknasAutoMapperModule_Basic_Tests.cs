using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.AutoMapper.SampleClasses;
using Tiknas.ObjectMapping;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.AutoMapper;

public class TiknasAutoMapperModule_Basic_Tests : TiknasIntegratedTest<AutoMapperTestModule>
{
    private readonly IObjectMapper _objectMapper;

    public TiknasAutoMapperModule_Basic_Tests()
    {
        _objectMapper = ServiceProvider.GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public void Should_Replace_IAutoObjectMappingProvider()
    {
        Assert.True(ServiceProvider.GetRequiredService<IAutoObjectMappingProvider>() is AutoMapperAutoObjectMappingProvider);
    }

    [Fact]
    public void Should_Get_Internal_Mapper()
    {
        _objectMapper.GetMapper().ShouldNotBeNull();
        _objectMapper.AutoObjectMappingProvider.GetMapper().ShouldNotBeNull();
    }

    [Fact]
    public void Should_Map_Objects_With_AutoMap_Attributes()
    {
        var dto = _objectMapper.Map<MyEntity, MyEntityDto>(new MyEntity { Number = 42 });
        dto.Number.ShouldBe(42);
    }

    //[Fact] TODO: Disabled because of https://github.com/AutoMapper/AutoMapper/pull/2379#issuecomment-355899664
    /*public void Should_Not_Map_Objects_With_AutoMap_Attributes()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            _objectMapper.Map<MyEntity, MyNotMappedDto>(new MyEntity {Number = 42});
        });
    }*/
}
