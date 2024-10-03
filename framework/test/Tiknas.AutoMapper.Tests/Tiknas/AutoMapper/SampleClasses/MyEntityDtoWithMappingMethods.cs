using System;
using Tiknas.ObjectMapping;

namespace Tiknas.AutoMapper.SampleClasses;

//TODO: Move tests to Tiknas.ObjectMapping test project
public class MyEntityDtoWithMappingMethods : IMapFrom<MyEntity>, IMapTo<MyEntity>
{
    public Guid Key { get; set; }

    public int No { get; set; }

    public MyEntityDtoWithMappingMethods()
    {

    }

    public MyEntityDtoWithMappingMethods(MyEntity entity)
    {
        MapFrom(entity);
    }

    public void MapFrom(MyEntity source)
    {
        Key = source.Id;
        No = source.Number;
    }

    MyEntity IMapTo<MyEntity>.MapTo()
    {
        return new MyEntity
        {
            Id = Key,
            Number = No
        };
    }

    void IMapTo<MyEntity>.MapTo(MyEntity destination)
    {
        destination.Id = Key;
        destination.Number = No;
    }
}
