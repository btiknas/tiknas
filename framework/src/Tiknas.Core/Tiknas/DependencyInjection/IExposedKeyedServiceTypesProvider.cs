using System;

namespace Tiknas.DependencyInjection;

public interface IExposedKeyedServiceTypesProvider
{
    ServiceIdentifier[] GetExposedServiceTypes(Type targetType);
}
