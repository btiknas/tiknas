using System;
using JetBrains.Annotations;
using Tiknas.Collections;
using Tiknas.DynamicProxy;

namespace Tiknas.DependencyInjection;

public class OnServiceRegistredContext : IOnServiceRegistredContext
{
    public virtual ITypeList<ITiknasInterceptor> Interceptors { get; }

    public virtual Type ServiceType { get; }

    public virtual Type ImplementationType { get; }

    public OnServiceRegistredContext(Type serviceType, [NotNull] Type implementationType)
    {
        ServiceType = Check.NotNull(serviceType, nameof(serviceType));
        ImplementationType = Check.NotNull(implementationType, nameof(implementationType));

        Interceptors = new TypeList<ITiknasInterceptor>();
    }
}
