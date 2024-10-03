using System.Collections.Generic;
using System.Reflection;
using Autofac.Core;
using Tiknas.DependencyInjection;

namespace Tiknas.Autofac;

public class TiknasPropertySelector : DefaultPropertySelector
{
    public TiknasPropertySelector(bool preserveSetValues)
        : base(preserveSetValues)
    {
    }

    public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
    {
        return propertyInfo.GetCustomAttributes(typeof(DisablePropertyInjectionAttribute), true).IsNullOrEmpty() &&
               base.InjectProperty(propertyInfo, instance);
    }

}
