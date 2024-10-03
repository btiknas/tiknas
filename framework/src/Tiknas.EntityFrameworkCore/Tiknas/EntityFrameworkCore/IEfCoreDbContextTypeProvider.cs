using System;

namespace Tiknas.EntityFrameworkCore;

public interface IEfCoreDbContextTypeProvider
{
    Type GetDbContextType(Type dbContextType);
}
