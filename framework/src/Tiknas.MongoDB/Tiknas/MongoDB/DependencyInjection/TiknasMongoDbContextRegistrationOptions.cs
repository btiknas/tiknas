using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.MongoDB.DependencyInjection;

public class TiknasMongoDbContextRegistrationOptions : TiknasCommonDbContextRegistrationOptions, ITiknasMongoDbContextRegistrationOptionsBuilder
{
    public TiknasMongoDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
    }
}
