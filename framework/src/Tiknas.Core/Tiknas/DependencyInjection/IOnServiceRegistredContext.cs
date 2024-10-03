using System;
using Tiknas.Collections;
using Tiknas.DynamicProxy;

namespace Tiknas.DependencyInjection;

public interface IOnServiceRegistredContext
{
    ITypeList<ITiknasInterceptor> Interceptors { get; }

    Type ImplementationType { get; }
}
