using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.MemoryDb.DependencyInjection;

public class TiknasMemoryDbContextRegistrationOptions : TiknasCommonDbContextRegistrationOptions, ITiknasMemoryDbContextRegistrationOptionsBuilder
{
    public TiknasMemoryDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
    }
}
