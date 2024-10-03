using AutoMapper;

namespace Tiknas.AutoMapper;

internal class MapperAccessor : IMapperAccessor
{
    public IMapper Mapper { get; set; } = default!;
}
