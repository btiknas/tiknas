using System;
using System.Collections.Generic;

namespace Tiknas.DependencyInjection;

public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
{
    public bool IsClassInterceptorsDisabled { get; set; }
}
