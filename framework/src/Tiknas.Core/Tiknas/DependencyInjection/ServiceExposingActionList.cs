using System;
using System.Collections.Generic;

namespace Tiknas.DependencyInjection;

public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
{

}
