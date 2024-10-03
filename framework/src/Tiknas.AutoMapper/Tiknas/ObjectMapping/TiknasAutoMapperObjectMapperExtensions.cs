using AutoMapper;
using Tiknas.AutoMapper;

namespace Tiknas.ObjectMapping;

public static class TiknasAutoMapperObjectMapperExtensions
{
    public static IMapper GetMapper(this IObjectMapper objectMapper)
    {
        return objectMapper.AutoObjectMappingProvider.GetMapper();
    }

    public static IMapper GetMapper(this IAutoObjectMappingProvider autoObjectMappingProvider)
    {
        if (autoObjectMappingProvider is AutoMapperAutoObjectMappingProvider autoMapperAutoObjectMappingProvider)
        {
            return autoMapperAutoObjectMappingProvider.MapperAccessor.Mapper;
        }

        throw new TiknasException($"Given object is not an instance of {typeof(AutoMapperAutoObjectMappingProvider).AssemblyQualifiedName}. The type of the given object it {autoObjectMappingProvider.GetType().AssemblyQualifiedName}");
    }
}
