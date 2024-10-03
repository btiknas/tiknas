using AutoMapper;
using Tiknas.TestApp.Testing;

namespace Tiknas.TestApp.Domain;

public class TestAutoMapProfile : Profile
{
    public TestAutoMapProfile()
    {
        CreateMap<PersonEto, Person>().ReverseMap();

        CreateMap<Product, ProductCacheItem>();
    }
}
