namespace Tiknas.ObjectMapping;

public interface IMapFrom<in TSource>
{
    void MapFrom(TSource source);
}
