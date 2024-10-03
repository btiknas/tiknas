using AutoMapper;
using Tiknas.ObjectExtending.TestObjects;

namespace Tiknas.AutoMapper.SampleClasses;

public class MyMapProfile : Profile
{
    public MyMapProfile()
    {
        CreateMap<MyEntity, MyEntityDto>().ReverseMap();

        CreateMap<ExtensibleTestPerson, ExtensibleTestPersonDto>()
            .MapExtraProperties(ignoredProperties: new[] { "CityName" });

        CreateMap<ExtensibleTestPerson, ExtensibleTestPersonWithRegularPropertiesDto>()
            .ForMember(x => x.Name, y => y.Ignore())
            .ForMember(x => x.Age, y => y.Ignore())
            .ForMember(x => x.IsActive, y => y.Ignore())
            .MapExtraProperties(mapToRegularProperties: true);
    }
}
