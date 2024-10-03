using System;

namespace Tiknas.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExposeKeyedServiceAttribute<TServiceType> : Attribute, IExposedKeyedServiceTypesProvider
    where TServiceType : class
{
    public ServiceIdentifier ServiceIdentifier { get; }

    public ExposeKeyedServiceAttribute(object serviceKey)
    {
        if (serviceKey == null)
        {
            throw new TiknasException($"{nameof(serviceKey)} can not be null! Use {nameof(ExposeServicesAttribute)} instead.");
        }

        ServiceIdentifier = new ServiceIdentifier(serviceKey, typeof(TServiceType));
    }

    public ServiceIdentifier[] GetExposedServiceTypes(Type targetType)
    {
        return new[] { ServiceIdentifier };
    }
}
