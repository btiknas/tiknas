using System;
using AutoMapper;

namespace Tiknas.AutoMapper;

public interface ITiknasAutoMapperConfigurationContext
{
    IMapperConfigurationExpression MapperConfiguration { get; }

    IServiceProvider ServiceProvider { get; }
}
