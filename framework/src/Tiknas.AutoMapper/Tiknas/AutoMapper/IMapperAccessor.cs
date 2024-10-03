using AutoMapper;

namespace Tiknas.AutoMapper;

public interface IMapperAccessor
{
    IMapper Mapper { get; }
}
