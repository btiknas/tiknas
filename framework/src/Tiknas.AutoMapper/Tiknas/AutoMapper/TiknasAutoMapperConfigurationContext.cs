using System;
using AutoMapper;

namespace Tiknas.AutoMapper;

public class TiknasAutoMapperConfigurationContext : ITiknasAutoMapperConfigurationContext
{
    public IMapperConfigurationExpression MapperConfiguration { get; }

    public IServiceProvider ServiceProvider { get; }

    public TiknasAutoMapperConfigurationContext(
        IMapperConfigurationExpression mapperConfigurationExpression,
        IServiceProvider serviceProvider)
    {
        MapperConfiguration = mapperConfigurationExpression;
        ServiceProvider = serviceProvider;
    }
}
