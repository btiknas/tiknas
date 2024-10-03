using System;

namespace Tiknas.DependencyInjection;

public interface IExposedServiceTypesProvider
{
    Type[] GetExposedServiceTypes(Type targetType);
}
